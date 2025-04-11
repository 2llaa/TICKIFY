using Microsoft.EntityFrameworkCore;
using TICKIFY.Data.Entities;

namespace TICKIFY.API.Persistence.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Hotels> Hotels { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<HotelReservations> HotelReservations { get; set; }
        public DbSet<Drivers> Drivers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);


            modelBuilder.Entity<Rooms>()
            .Property(r => r.Type)
            .HasConversion<string>();

            modelBuilder.Entity<Drivers>()
            .Property(d => d.CarType)
            .HasConversion<string>();





        }
    }
}
