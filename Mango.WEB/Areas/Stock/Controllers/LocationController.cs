using Mango.WEB.Adapters.Stock;
using Mango.WEB.Areas.Stock.Models.Location;
using Mango.WEB.Helpers;
using Mango.WEB.Interfaces.Managers.Stock;
using Mango.WEB.Models;
using Mango.WEB.Models.Base.Request;
using Mango.WEB.Models.Base.Response;
using Mango.WEB.Models.Stock.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Threading.Tasks;

namespace Mango.WEB.Areas.Stock.Controllers
{
    [Area("Stock")]
    public class LocationController : Controller
    {
        private readonly ILocationManager __LocationManager;
        private readonly UserManager<IdentityUser> __UserManager;
        private const string ENTITY_NAME = "Kitchen";

        public LocationController(ILocationManager locationManager, UserManager<IdentityUser> userManager)
        {
            __LocationManager = locationManager ?? throw new ArgumentNullException(nameof(locationManager));
            __UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateModal()
        {
            string _UserIDString = __UserManager.GetUserId(User);
            if (Guid.TryParse(_UserIDString, out Guid userUID))
            {
                return PartialView("_CreateLocation", new CreateViewModel { UserUID = userUID });
            }

            return Json(new ErrorActionModel { Message = $"{GlobalConstants.ERROR_ACTION_PREFIX} retrieve user details to create {ENTITY_NAME}" });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Invalid form submission.";
                return PartialView("_CreateLocation", viewModel);
            }

            LocationResponse _Response = await __LocationManager.CreateAsync(viewModel.ToRequest());

            if (!_Response.Success)
            {
                ViewData["ErrorMessage"] = _Response.ErrorMessage;
                return CreateModal();
            }
            else
            {
                ViewData["SuccessMessage"] = $"{GlobalConstants.SUCCESS_ACTION_PREFIX} created {ENTITY_NAME}";
            }

            return Json(0);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateModal(Guid locationUID)
        {
            string _UserIDString = __UserManager.GetUserId(User);
            if (Guid.TryParse(_UserIDString, out Guid userUID))
            {
                LocationResponse _Response = await __LocationManager.GetAsync(new UIDRequest { UID = locationUID });

                if (!_Response.Success)
                {
                    return Json(new ErrorActionModel { Message = _Response.ErrorMessage });
                }

                return PartialView("_EditLocation", _Response.ToEditViewModel());
            }

            return RedirectToAction("Index", "Stock", new { Area = "Stock", errorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} retrieve user details to modify {ENTITY_NAME}" });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Invalid form submission.";
                return PartialView("_EditLocation", viewModel);
            }

            BaseResponse _Response = new BaseResponse();
            string _UserIDString = __UserManager.GetUserId(User);
            if (Guid.TryParse(_UserIDString, out Guid userUID))
            {
                _Response = await __LocationManager.UpdateAsync(viewModel.ToUpdateRequest(userUID));
            }
            else
            {
                _Response.Success = false;
                _Response.ErrorMessage = $"You do not have permission to update the {ENTITY_NAME}";
            }

            if (!_Response.Success)
            {
                ViewData["ErrorMessage"] = _Response.ErrorMessage;
                return await UpdateModal(viewModel.LocationUID);
            }

            return Json(0);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteModal(Guid locationUID)
        {
            string _UserIDString = __UserManager.GetUserId(User);
            if (Guid.TryParse(_UserIDString, out Guid userUID))
            {
                LocationResponse _Response = await __LocationManager.GetAsync(new UIDRequest { UID = locationUID });

                if (!_Response.Success)
                {
                    return RedirectToAction("Index", "Stock", new { Area = "Stock", errorMessage = _Response.ErrorMessage });
                }

                return PartialView("_DeleteLocation", new DeleteViewModel { Location = _Response.ToViewModel() });
            }

            return RedirectToAction("Index", "Stock", new { Area = "Stock", errorMessage = $"{GlobalConstants.ERROR_ACTION_PREFIX} retrieve user details to delete {ENTITY_NAME}" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(DeleteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Invalid form submission.";
                return PartialView("_DeleteLocation", viewModel);
            }

            BaseResponse _Response = new BaseResponse();
            string _UserIDString = __UserManager.GetUserId(User);
            if (Guid.TryParse(_UserIDString, out Guid userUID))
            {
                _Response = await __LocationManager.DeleteAsync(new UserUIDAndUIDRequest { UID = viewModel.Location.UID, UserUID = userUID });
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
    }
}
