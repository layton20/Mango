using Mango.WEB.Adapters.Stock;
using Mango.WEB.Areas.Stock.Models.Stock;
using Mango.WEB.Interfaces.Managers.Stock;
using Mango.WEB.Models.Stock.Request;
using Mango.WEB.Models.Stock.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.WEB.Areas.Stock.Controllers
{
    [Area("Stock")]
    public class StockController : Controller
    {
        private readonly UserManager<IdentityUser> __UserManager;
        private readonly IStockManager __StockManager;
        private readonly ILocationManager __LocationManager;

        public StockController(UserManager<IdentityUser> userManager, IStockManager stockManager, ILocationManager locationManager)
        {
            __UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            __StockManager = stockManager ?? throw new ArgumentNullException(nameof(stockManager));
            __LocationManager = locationManager ?? throw new ArgumentNullException(nameof(locationManager));
        }

        public async Task<IActionResult> Index(string errorMessage = "", string successMessage = "")
        {
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                ViewData["ErrorMessage"] = errorMessage;
            }
            if (!string.IsNullOrWhiteSpace(successMessage))
            {
                ViewData["SuccessMessage"] = successMessage;
            }

            LocationsResponse _Response = new LocationsResponse();

            string _UserIDString = __UserManager.GetUserId(User);
            if (Guid.TryParse(_UserIDString, out Guid userUID))
            {
                _Response = await __LocationManager.GetByUserAsync(new GetLocationsByUserRequest { UID = userUID });
            }

            IndexViewModel _ViewModel = new IndexViewModel
            {
                Kitchens = _Response?.Locations?.ToViewModel().ToList() ?? Enumerable.Empty<LocationViewModel>().ToList(),
                UnassignedStocks = new UnassignedStockViewModel()
            };

            return View(_ViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
