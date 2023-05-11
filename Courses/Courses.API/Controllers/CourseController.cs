using Courses.Application.Services;
using Courses.DataTransferObjects.Requests;
using Courses.DataTransferObjects.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Text;

namespace Courses.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //http://localhost:7115/api/Course
    public class CourseController : ControllerBase
    {

        private readonly ICourseService courseService;
        private readonly ILogger<CourseController> logger;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;

        public CourseController(ICourseService courseService, ILogger<CourseController> logger, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.courseService = courseService;
            this.logger = logger;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (!memoryCache.TryGetValue("allCourses", out CacheBenchmark cacheBenchmark))
            {
                var options = new MemoryCacheEntryOptions()
                                  .SetSlidingExpiration(TimeSpan.FromMinutes(1))
                                  .RegisterPostEvictionCallback((key, value, evictionReason, state) =>
                                  {
                                      /*
                                       * bu options nesnesinin kullanılıdığı memory-cache içinden cache data çıkarıldığında
                                       * bu metot devreye girecek.
                                       */
                                  });
                cacheBenchmark = new CacheBenchmark
                {
                    Courses = await courseService.GetCoursesAsync(),
                    CacheTime = DateTime.UtcNow

                };

                memoryCache.Set("allCourses", cacheBenchmark, options);

            }

            logger.LogInformation($"{cacheBenchmark.CacheTime} tarihinde, get request çalıştı");
            return Ok(cacheBenchmark.Courses);
        }


        public string CachedTime { get; set; }

        [HttpGet("[action]/{name}")]

        public async Task<IActionResult> Search(string name)
        {

            var array = distributedCache.Get("cachedTime");
            if (array != null)
            {
                CachedTime = Encoding.UTF8.GetString(array);
            }
            else
            {
                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.UtcNow.AddMinutes(5),
                };

                var cachedTime = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
                distributedCache.Set("cachedTime", cachedTime, options);
                CachedTime = DateTime.Now.ToString();
            }




            IEnumerable<CourseSummaryResponse> findedCourses;
            if (distributedCache.Get("searchCache") != null)
            {
                var searchArray = distributedCache.Get("searchCache");
                var result = Encoding.UTF8.GetString(searchArray);
                findedCourses = JsonConvert.DeserializeObject<IEnumerable<CourseSummaryResponse>>(result);

            }
            else
            {
                findedCourses = await courseService.SearchCourse(name);
                var jsonSerialize = JsonConvert.SerializeObject(findedCourses);
                distributedCache.Set("searchCache", Encoding.UTF8.GetBytes(jsonSerialize), new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(1)));
            }

            logger.LogWarning($"Dikkat: cache içindeki tarih: {CachedTime}");
            return Ok(findedCourses);
        }
        [HttpGet("{id}")]
        [ResponseCache(Duration = 70, VaryByQueryKeys = new[] { "id" }, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> Get(int id)
        {
            var course = await courseService.GetCourse(id);
            return Ok(new { course = course, date = DateTime.Now.ToString() });
        }


        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateNewCourseRequest request)
        {
            if (ModelState.IsValid)
            {
                int createdCourseId = await courseService.CreateNewCourse(request);
                return CreatedAtAction(nameof(Get), routeValues: new { id = createdCourseId }, null);
            }

            return BadRequest(ModelState);
        }
        /*
         * idempotent: Bir fonksiyon arka arkaya çalıştırılığında yan etki olmaksızın belirlenmiş sonujçları vermeli.
         */
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id,
                                                [FromBody] UpdateCourseRequest request)

        {
            if (ModelState.IsValid)
            {
                await courseService.UpdateCourse(request);
                return Ok();
            }
            return BadRequest(ModelState);
        }




    }
}
