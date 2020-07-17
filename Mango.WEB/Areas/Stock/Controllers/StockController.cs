using Mango.WEB.Adapters.Stock;
using Mango.WEB.Areas.Stock.Models.Stock;
using Mango.WEB.Interfaces.Managers.Stock;
using Mango.WEB.Models.Stock.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.WEB.Areas.Stock.Controllers
{
    public class StockController : Controller
    {
        private readonly IStockManager __StockManager;

        public StockController(IStockManager stockManager)
        {
            __StockManager = stockManager ?? throw new ArgumentNullException(nameof(stockManager));
        }

        public async Task<IActionResult> Index()
        {
            StocksResponse _Response = await __StockManager.GetAsync();

            IndexViewModel _ViewModel = new IndexViewModel
            {
                Stocks = _Response?.Stocks?.ToEntity().ToList()
            };

            return View(_ViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
