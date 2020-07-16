using System;
using System.ComponentModel.DataAnnotations;

namespace Mango.WEB.Entities
{
    public abstract class TimestampEntity
    {
        public TimestampEntity()
        {
            DateTime _CurrentTime = new DateTime();
            CreatedTimestamp = _CurrentTime;
            AmendedTimestamp = _CurrentTime;
        }

        [Required]
        public DateTime CreatedTimestamp { get; set; }
        [Required]
        public DateTime AmendedTimestamp { get; set; }
    }
}
