using System.ComponentModel.DataAnnotations;

namespace Mango.WEB.Entities.Note
{
    public class NoteEntity : ItemEntity
    {
        [Required]
        public string Note { get; set; }
    }
}
