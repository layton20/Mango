using Mango.WEB.Models.Base.Response;
using System.Collections.Generic;

namespace Mango.WEB.Models.Stock.Response
{
    public class StocksResponse : BaseResponse
    {
        public IList<StockResponse> Stocks { get; set; }
    }
}
