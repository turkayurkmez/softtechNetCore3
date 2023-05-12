using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Course.Tests
{
    public class CourseControllerTest : IClassFixture<InMemoryWebApplicationFactory<Program>>
    {

        private InMemoryWebApplicationFactory<Program> _factory;

        public CourseControllerTest(InMemoryWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void api_success_test()
        {
            //TODO 1: Önce, bir istemci nesnesi olacak (client). Bu nesne, Hem Db hem de http protokolünü taklit edebilmeli
            //TODO 2: Bu client nesnesi, endpoint'e request gönderecek ve response alabilecek
            //TODO 3: Gelen response OK (StatusCode = 200) olacak
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/Course");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);


        }
        [Fact]
        public async void api_post_test()
        {
            var course = new Courses.Entities.Course
            {
                Name = "Test Course",
                CourseImage = "Test",
                Description = "Test",
                TotalHours = 1,
            };
            var client = _factory.CreateClient();
            var httpContent = new StringContent(JsonConvert.SerializeObject(course), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/Course", httpContent);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(response.Headers.Location);

        }

        [Fact]
        public async void post_invalid_model_return_bad_request()
        {
            var course = new Courses.Entities.Course
            {
                Name = string.Empty,
                CourseImage = string.Empty,
                Description = "Test",
                TotalHours = 1,
            };
            var client = _factory.CreateClient();
            var httpContent = new StringContent(JsonConvert.SerializeObject(course), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/Course", httpContent);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }
    }
}