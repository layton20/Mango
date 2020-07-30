using Mango.WEB.Entities.Booking;
using Mango.WEB.Entities.Note;
using Mango.WEB.Entities.Stock;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mango.WEB.Models
{
    public class ApplicationContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        // Booking
        public DbSet<BookingEntity> Bookings { get; set; }

        // Stock
        public DbSet<StockEntity> Stocks { get; set; }
        public DbSet<LocationEntity> Locations { get; set; }
        public DbSet<StockLocationEntity> StockLocations { get; set; }

        // Note
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<NoteEntity> Notes { get; set; }
        public DbSet<BookNoteEntity> BookNotes { get; set; }
        public DbSet<DietEntity> Diets { get; set; }
    }
}
