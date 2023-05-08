using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using usingDI.Models;
using usingDI.Services;

namespace usingDI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService productService;
        private readonly ISingleton _singleton;
        private readonly ITransient _transient;
        private readonly IScoped _scoped;
        private readonly GuidService _guidService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ISingleton singleton, ITransient transient, IScoped scoped, GuidService guidService)
        {
            _logger = logger;
            this.productService = productService;
            _singleton = singleton;
            _scoped = scoped;
            _transient = transient;
            _guidService = guidService;

        }

        public IActionResult Index()
        {
            var products = productService.GetProducts();
            return View(products);
        }

        public IActionResult Privacy()
        {
            ViewBag.Singleton = _singleton.Guid.ToString();
            ViewBag.Transient = _transient.Guid.ToString();
            ViewBag.Scoped = _scoped.Guid.ToString();


            ViewBag.SingletonService = _guidService.Singleton.Guid.ToString();
            ViewBag.TransientService = _guidService.Transient.Guid.ToString();
            ViewBag.ScopedService = _guidService.Scoped.Guid.ToString();



            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}