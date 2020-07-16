using Mango.WEB.Models.Base.Response;

namespace Mango.WEB.Models.Stock.Response
{
    public class StockResponse : EntityResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}
