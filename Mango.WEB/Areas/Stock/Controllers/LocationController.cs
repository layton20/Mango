using Mango.WEB.Adapters.Stock;
using Mango.WEB.Areas.Stock.Models.Location;
using Mango.WEB.Helpers;
using Mango.WEB.Interfaces.Managers.Stock;
using Mango.WEB.Models;
using Mango.WEB.Models.Stock.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    }
}
