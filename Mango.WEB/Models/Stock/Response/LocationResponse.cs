using Mango.WEB.Models.Base.Response;
using System;

namespace Mango.WEB.Models.Stock.Response
{
    public class LocationResponse : ItemResponse
    {
        public string Floor { get; set; }
        public string Address { get; set; }
        public Guid UserUID { get; set; }
    }
}
