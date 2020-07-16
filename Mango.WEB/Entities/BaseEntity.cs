using System;
using System.ComponentModel.DataAnnotations;

namespace Mango.WEB.Entities
{
    public abstract class BaseEntity : TimestampEntity
    {
        public BaseEntity()
        {
            UID = Guid.NewGuid();
        }

        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        public Guid UID { get; set; }
    }
}
