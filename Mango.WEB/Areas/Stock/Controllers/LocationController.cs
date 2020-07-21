using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mango.WEB.Adapters.Stock;
using Mango.WEB.Areas.Stock.Models.Location;
using Mango.WEB.Helpers;
using Mango.WEB.Interfaces.Managers.Stock;
using Mango.WEB.Models.Stock.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Mango.WEB.Areas.Stock.Controllers
{
    [Area("Stock")]
    public class LocationController : Controller
    {
        private readonly ILocationManager __LocationManager;
        private const string ENTITY_NAME = "Kitchen";

        public LocationController(ILocationManager locationManager)
        {
            __LocationManager = locationManager ?? throw new ArgumentNullException(nameof(locationManager));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateModal()
        {
            return PartialView("_CreateLocation", new CreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Invalid form submission.";
                return CreateModal();
            }

            LocationResponse _Response = await __LocationManager.CreateAsync(viewModel.ToRequest());

            if (!_Response.Success)
            {
                ViewData["ErrorMessage"] = _Response.ErrorMessage;
                return CreateModal();
            }
            else
            {
                ViewData["SuccessMessage"] = $"{GlobalConstants.SUCCESS_ACTION_PREFIX} created Kitchen";
            }

            return RedirectToAction("Index", "Stock", new { Area = "Stock" });
        }
    }
}
