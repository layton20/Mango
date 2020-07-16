using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mango.WEB.Models.Base.Request
{
    public class BulkUIDRequest
    {
        [Required]
        public IList<Guid> UIDs { get; set; }
    }
}
