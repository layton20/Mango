using System;
using System.ComponentModel.DataAnnotations;

namespace Mango.WEB.Models.Stock.Request
{
    public class AssignToLocationRequest
    {
        [Required]
        public Guid StockUID { get; set; }
        [Required]
        public Guid LocationUID { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
