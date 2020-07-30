using System;
using System.ComponentModel.DataAnnotations;

namespace Mango.WEB.Entities.Stock
{
    public class StockLocationEntity : BaseEntity
    {
        [Required]
        public Guid LocationUID { get; set; }
        [Required]
        public Guid StockUID { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
