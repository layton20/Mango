using Mango.WEB.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mango.WEB.Entities.Stock
{
    public class StockEntity : ItemEntity
    {
        [Required]
        public Guid UserUID { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        public StockType StockType { get; set; }
    }
}
