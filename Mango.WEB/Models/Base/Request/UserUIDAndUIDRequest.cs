using System;
using System.ComponentModel.DataAnnotations;

namespace Mango.WEB.Models.Base.Request
{
    public class UserUIDAndUIDRequest : UIDRequest
    {
        [Required]
        public Guid UserUID { get; set; }
    }
}
