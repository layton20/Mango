using System;
using System.ComponentModel.DataAnnotations;

namespace Mango.WEB.Models.Base.Request
{
    public class UIDRequest
    {
        [Required]
        public Guid UID { get; set; }
    }
}
