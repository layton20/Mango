using System;
using System.ComponentModel.DataAnnotations;

namespace Mango.WEB.Entities.Note
{
    public class BookNoteEntity : BaseEntity
    {
        [Required]
        public Guid BookUID { get; set; }
        [Required]
        public Guid NoteUID { get; set; }
    }
}
