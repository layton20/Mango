using Mango.WEB.Areas.Admin.Models.Dashboard;
using Microsoft.AspNetCore.Mvc;

namespace Mango.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            IndexViewModel _ViewModel = new IndexViewModel();

            return View(_ViewModel);
        }
    }
}
