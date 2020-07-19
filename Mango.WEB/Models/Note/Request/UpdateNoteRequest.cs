using Mango.WEB.Models.Base.Request;
using System.ComponentModel.DataAnnotations;

namespace Mango.WEB.Models.Note.Request
{
    public class UpdateNoteRequest : UIDRequest
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Note { get; set; }
    }
}
