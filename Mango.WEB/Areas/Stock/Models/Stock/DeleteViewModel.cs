using Mango.WEB.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mango.WEB.Areas.Stock.Models.Stock
{
    public class DeleteViewModel
    {
        [Required]
        public Guid UID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        public StockType StockType { get; set; }
        public DateTime AmendedTimestamp { get; set; }
    }
}
