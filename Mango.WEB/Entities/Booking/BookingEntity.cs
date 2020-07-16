using System;
using System.ComponentModel.DataAnnotations;

namespace Mango.WEB.Entities.Booking
{
    public class BookingEntity : ItemEntity
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Attendees { get; set; }
        public string CustomerNote { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public int Duration { get; set; }
    }
}
