using System.ComponentModel.DataAnnotations;

namespace Mango.WEB.Entities
{
    public abstract class ItemEntity : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
