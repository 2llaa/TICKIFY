using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Tickfy.Entities;

using System.Reflection;
using System.Security.Claims;
namespace Tickfy.Persistence;

public class TickfyDBContext(DbContextOptions<TickfyDBContext> options,
    IHttpContextAccessor httpContextAccessor):
    IdentityDbContext<ApplicationUser>(options)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public DbSet<Flight> Flights { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<FlightReservation> FlightReservationS { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<HotelReservation> HotelReservations { get; set; }
    public DbSet<HotelReservationRoom> HotelReservationRoomss { get; set; }
    public DbSet<BedInfo> BedInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TickfyDBContext).Assembly);

        base.OnModelCreating(modelBuilder);

    }

    //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    //{

    //    var entries = ChangeTracker.Entries<AuditableEntity>();

    //    foreach (var entry in entries)
    //    {
    //        var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    //        if (entry.State == EntityState.Added)
    //        {
    //            entry.Property(e => e.CreatedById).CurrentValue = currentUserId!;
    //        }
    //        else if (entry.State == EntityState.Modified)
    //        {
    //            entry.Property(e => e.UpdatedById).CurrentValue = currentUserId!;
    //            entry.Property(e => e.UpdatedOn).CurrentValue = DateTime.UtcNow;
    //        }

    //    }

    //    return base.SaveChangesAsync(cancellationToken);
    //}
}
