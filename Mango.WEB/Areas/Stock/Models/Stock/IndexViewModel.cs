using Mango.WEB.Areas.Stock.Models.Location;
using System.Collections.Generic;

namespace Mango.WEB.Areas.Stock.Models.Stock
{
    public class IndexViewModel
    {
        public IList<LocationViewModel> Kitchens { get; set; }
        public UnassignedStockViewModel UnassignedStocks { get; set; }
    }
}
