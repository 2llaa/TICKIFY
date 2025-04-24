using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TICKIFY.Data.Entities;

namespace TICKIFY.API.Persistence.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
          {
          }
        public DbSet<Hotels> Hotels { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<HotelReservations> HotelReservations { get; set; }
        public DbSet<Drivers> Drivers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<Rooms>()
            .Property(r => r.Type)
            .HasConversion<string>();
            modelBuilder.Entity<Rooms>()
                .Property(r => r.Status)
                .HasConversion<string>();
            modelBuilder.Entity<HotelReservations>()
                .Property(r => r.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Drivers>()
            .Property(d => d.CarType)
            .HasConversion<string>();

        }
    }
}
