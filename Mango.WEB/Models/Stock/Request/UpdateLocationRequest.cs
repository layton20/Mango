using Mango.WEB.Models.Base.Request;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mango.WEB.Models.Stock.Request
{
    public class UpdateLocationRequest : UIDRequest
    {
        [Required]
        public Guid UserUID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [MaxLength(50)]
        public string Floor { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
