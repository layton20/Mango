using Mango.WEB.Adapters.Stock;
using Mango.WEB.Areas.Stock.Models.Stock;
using Mango.WEB.Helpers;
using Mango.WEB.Interfaces.Managers.Stock;
using Mango.WEB.Models;
using Mango.WEB.Models.Base.Request;
using Mango.WEB.Models.Base.Response;
using Mango.WEB.Models.Stock.Request;
using Mango.WEB.Models.Stock.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using NsModelsLocation = Mango.WEB.Areas.Stock.Models.Location;

namespace Mango.WEB.Areas.Stock.Controllers
{
    [Area("Stock")]
    public class StockController : Controller
    {
        private readonly UserManager<IdentityUser> __UserManager;
        private readonly IStockManager __StockManager;
        private readonly ILocationManager __LocationManager;
        private const string ENTITY_NAME = "Stock";

        public StockController(UserManager<IdentityUser> userManager, IStockManager stockManager, ILocationManager locationManager)
        {
            __UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            __StockManager = stockManager ?? throw new ArgumentNullException(nameof(stockManager));
            __LocationManager = locationManager ?? throw new ArgumentNullException(nameof(locationManager));
        }

        [HttpGet]
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

            LocationsResponse _LocationsResponse = new LocationsResponse();
            StocksResponse _UnassignedStocks = new StocksResponse();

            Guid _UserID = GetLoggedUserID();
            if (_UserID != Guid.Empty)
            {
                _LocationsResponse = await __LocationManager.GetByUserAsync(new GetLocationsByUserRequest { UID = _UserID });
                _UnassignedStocks = await __StockManager.GetByUserAsync(new GetStocksByUserRequest { UserUID = _UserID });
            }

            IndexViewModel _ViewModel = new IndexViewModel
            {
                Kitchens = _LocationsResponse?.Locations?.ToViewModel().ToList() ?? Enumerable.Empty<NsModelsLocation.LocationViewModel>().ToList(),
                UnassignedStocks = _UnassignedStocks?.Stocks?.ToViewModel().ToList() ?? Enumerable.Empty<StockViewModel>().ToList()
            };

            return View(_ViewModel);
        }

        [HttpGet]
        public IActionResult CreateModal()
        {
            return PartialView("_CreateStock", new CreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Invalid form submission.";
                return PartialView("_CreateStock", viewModel);
            }

            Guid _UserID = GetLoggedUserID();
            if (_UserID == Guid.Empty)
            {
                return Json(new { error = $"{GlobalConstants.ERROR_ACTION_PREFIX} retrieve user details to create {ENTITY_NAME}" });
            }
            
            StockResponse _Response = await __StockManager.CreateAsync(viewModel.ToRequest(_UserID));

            if (!_Response.Success)
            {
                ViewData["ErrorMessage"] = _Response.ErrorMessage;
                return PartialView("_CreateStock", new CreateViewModel());
            }

            return Json(new { success = $"{GlobalConstants.SUCCESS_ACTION_PREFIX} created {ENTITY_NAME}" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateModal(Guid stockUID)
        {
            Guid _UserID = GetLoggedUserID();
            if (_UserID == Guid.Empty)
            {
                return RedirectToAction("Index", "Stock", new { Area = "Stock", errorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} retrieve user details to modify {ENTITY_NAME}" });
            }

            StockResponse _Response = await __StockManager.GetAsync(new UIDRequest { UID = stockUID });

            if (!_Response.Success)
            {
                return Json(new ErrorActionModel { Message = _Response.ErrorMessage });
            }

            return PartialView("_EditStock", _Response.ToEditViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Invalid form submission.";
                return PartialView("_EditStock", viewModel);
            }

            BaseResponse _Response = new BaseResponse();
            Guid _UserID = GetLoggedUserID();
            if (_UserID == Guid.Empty)
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"You do not have permission to update the {ENTITY_NAME}";
            }
            else
            {
                _Response = await __StockManager.UpdateAsync(viewModel.ToUpdateRequest(_UserID));
            }

            if (!_Response.Success)
            {
                ModelState.AddModelError("Error", _Response.ErrorMessage);
                return await UpdateModal(viewModel.StockUID);
            }

            return Json(new { success = $"{GlobalConstants.SUCCESS_ACTION_PREFIX} updated {ENTITY_NAME}" });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteModal(Guid stockUID)
        {
            Guid _UserID = GetLoggedUserID();
            if (_UserID == Guid.Empty)
            {
                return RedirectToAction("Index", "Stock", new { Area = "Stock", errorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} retrieve user details to delete {ENTITY_NAME}" });
            }

            StockResponse _Response = await __StockManager.GetAsync(new UIDRequest { UID = stockUID });

            if (!_Response.Success)
            {
                return RedirectToAction("Index", "Stock", new { Area = "Stock", errorMessage = _Response.ErrorMessage });
            }

            return PartialView("_DeleteStock", _Response.ToDeleteViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(DeleteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Invalid form submission.";
                return PartialView("_DeleteStock", viewModel);
            }

            BaseResponse _Response = new BaseResponse();
            string _UserIDString = __UserManager.GetUserId(User);
            if (Guid.TryParse(_UserIDString, out Guid userUID))
            {
                _Response = await __StockManager.DeleteAsync(new UserUIDAndUIDRequest { UID = viewModel.UID, UserUID = userUID });
            }
            else
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"You do not have permission to delete the {ENTITY_NAME}";
            }

            if (!_Response.Success)
            {
                return RedirectToAction("Index", "Stock", new { Area = "Stock", errorMessage = _Response.ErrorMessage });
            }

            return RedirectToAction("Index", "Stock", new { Area = "Stock", successMessage = $"{GlobalConstants.SUCCESS_ACTION_PREFIX} deleted {ENTITY_NAME}" });
        }

        private Guid GetLoggedUserID()
        {
            string _UserIDString = __UserManager.GetUserId(User);

            if (Guid.TryParse(_UserIDString, out Guid userUID))
            {
                return userUID;
            }

            return Guid.Empty;
        }
    }
}
