using System;
using System.ComponentModel.DataAnnotations;

namespace Mango.WEB.Models.Note.Request
{
    public class UpdateDietRequest
    {
        [Required]
        public Guid UID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
