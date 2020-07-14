using System.ComponentModel.DataAnnotations;

namespace Mango.WEB.Entities.Note
{
    public class NoteEntity : BaseEntity
    {
        [Required]
        public string Note { get; set; }
    }
}
