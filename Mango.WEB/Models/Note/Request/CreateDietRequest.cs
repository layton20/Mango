using System.ComponentModel.DataAnnotations;

namespace Mango.WEB.Models.Note.Request
{
    public class CreateDietRequest
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
