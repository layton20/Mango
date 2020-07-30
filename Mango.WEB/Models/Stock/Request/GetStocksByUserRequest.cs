using Mango.WEB.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mango.WEB.Models.Stock.Request
{
    public class GetStocksByUserRequest
    {
        public StockType StockType { get; set; }
        [Required]
        public Guid UserUID { get; set; }
    }
}
