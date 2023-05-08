using Microsoft.AspNetCore.Mvc;
using usingDI.Tenants;

namespace usingDI.Controllers
{
    public class SampleController : Controller
    {

        private readonly IDatabaseClient databaseClient;

        public SampleController(IDatabaseClient databaseClient)
        {
            this.databaseClient = databaseClient;
        }

        public IActionResult Index()
        {
            ViewBag.Result = databaseClient.ToString();

            return View();
        }
    }
}
