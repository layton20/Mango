using Mango.WEB.Models.Base.Response;
using Mango.WEB.Models.Enums;

namespace Mango.WEB.Models.Stock.Response
{
    public class StockResponse : EntityResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public StockType StockType { get; set; }
    }
}
