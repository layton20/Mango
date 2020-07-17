using Mango.WEB.Entities.Stock;
using System.Collections.Generic;

namespace Mango.WEB.Areas.Stock.Models.Stock
{
    public class IndexViewModel
    {
        public IList<StockEntity> Stocks { get; set; }
    }
}
