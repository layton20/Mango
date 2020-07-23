using System;

namespace Mango.WEB.Areas.Stock.Models.Stock
{
    public class LocationViewModel
    {
        public Guid UID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Floor { get; set; }
        public string Address { get; set; }
        public Guid UserUID { get; set; }
    }
}
