using CustomTagBuilder.Models;
using CustomTagBuilder.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CustomTagBuilder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CourseService _courseService;

        public HomeController(ILogger<HomeController> logger, CourseService courseService)
        {
            _logger = logger;
            _courseService = courseService;
        }

        public IActionResult Index(int? pageNo = 1)
        {
            var courses = _courseService.GetCourses();
            var courseCount = courses.Count();
            var coursePerPage = 4;
            var pages = Math.Ceiling((double)courseCount / coursePerPage);

            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = pageNo.Value,
                ItemsPerPage = coursePerPage,
                TotalItemsCount = courseCount,
            };

            //ViewBag.Pages = pages;
            var pagingCourses = courses.OrderBy(c => c.Id)
                                       .Skip((pageNo.Value - 1) * coursePerPage)
                                       .Take(coursePerPage)
                                       .ToList();

            var model = new CoursesViewModel
            {
                Courses = pagingCourses,
                PagingInfo = pagingInfo
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}