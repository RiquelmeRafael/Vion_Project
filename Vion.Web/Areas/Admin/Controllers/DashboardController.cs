using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vion.Web.Areas.Admin.Services;

namespace Vion.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly DashboardService _service;

        public DashboardController(DashboardService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetDashboardDataAsync();
            return View(data);
        }
    }
}
