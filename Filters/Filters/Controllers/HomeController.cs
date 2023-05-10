using Filters.Filters;
using Filters.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Filters.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /*
         * Authorization Filter
         *   - İlk çalışan filtre türü
         * Resource Filter
         *   - Authorization'dan sonra çalışır...
         *   - Aspect Oriented (Cross-cutting) ihtiyaçlarda kullanılr. Örneğin; model binding sırasında ve sonrasında gibi...
         * Action Filter
         *   - Action çalışmaya başlamadan hemen önce ve sonra devreye girer 
         *   - Parametrelerini yakalayabilir.
         *   - Action'un çıktısına müdahale edebilir.
         * Endpoint Filter
         *   - Action çalışmaya başlamadan hemen önce ve sonra devreye girer 
         *   - hem action hem de router yönlendirmelerinde tetiklenir.
         * Exception Filter
         *   - Action ÇALIŞIRKEN hata meydana gelirse tetiklenir.
         * Result Filter
         *   - Action çalışmaya bitirdikten sonra devreye girer 
         *   - Action başarılı olduktan sonra çalışır
         *   - Örnek: Çıktının formatı ya da View'da kullanacağınız bir değer....
         * 
         */
        [Stopwatch]
        [SampleFilter]
        public IActionResult Index()
        {
            return View();
        }

        [Stopwatch]
        [NotImplemented]
        public IActionResult Privacy()
        {
            PrivacyModel privacyModel = new PrivacyModel();
            privacyModel.PrivacyInfo = "Bu uygulamada, çerez kullanılmaktadır";
            throw new NotImplementedException();
            return View(privacyModel);
        }

        public IActionResult Test()
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