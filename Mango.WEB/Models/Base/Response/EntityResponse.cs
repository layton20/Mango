using System;

namespace Mango.WEB.Models.Base.Response
{
    public class EntityResponse : BaseResponse
    {
        public Guid UID { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime AmendedTimestamp { get; set; }
    }
}
