using Mango.WEB.Models.Base.Request;
using Mango.WEB.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mango.WEB.Models.Stock.Request
{
    public class CreateStockRequest
    {
        public CreateStockRequest()
        {
            Quantity = 0;
        }

        [Required]
        public Guid UserUID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public StockType StockType { get; set; }
    }
}
