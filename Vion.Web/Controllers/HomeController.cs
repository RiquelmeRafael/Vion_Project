using Microsoft.AspNetCore.Mvc;

namespace Vion.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Catalog()
        {
            return View();
        }
    }
}
