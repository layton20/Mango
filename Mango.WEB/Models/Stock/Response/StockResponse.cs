using Mango.WEB.Models.Base.Response;
using Mango.WEB.Models.Enums;

namespace Mango.WEB.Models.Stock.Response
{
    public class StockResponse : ItemResponse
    {
        public int Quantity { get; set; }
        public StockType StockType { get; set; }
    }
}
