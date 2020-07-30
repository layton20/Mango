using System;

namespace Mango.WEB.Entities.Stock
{
    public class LocationEntity : ItemEntity
    {
        public Guid UserUID { get; set; }
        public string Floor { get; set; }
        public string Address { get; set; }
    }
}
