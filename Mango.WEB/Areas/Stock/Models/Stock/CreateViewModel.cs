using Mango.WEB.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mango.WEB.Areas.Stock.Models.Stock
{
    public class CreateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        [Display(Name = "Stock Type")]
        public StockType StockType { get; set; }
    }
}
