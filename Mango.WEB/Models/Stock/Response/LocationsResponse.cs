using Mango.WEB.Models.Base.Response;
using System.Collections.Generic;

namespace Mango.WEB.Models.Stock.Response
{
    public class LocationsResponse : BaseResponse
    {
        public LocationsResponse()
        {
            Locations = new List<LocationResponse>();
        }

        public IList<LocationResponse> Locations { get; set; }
    }
}
