using Mango.WEB.Models.Base.Response;

namespace Mango.WEB.Models.Stock.Response
{
    public class LocationResponse : ItemResponse
    {
        public string Floor { get; set; }
        public string Address { get; set; }
    }
}
