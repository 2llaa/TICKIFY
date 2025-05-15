using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tickfy.Entities;

namespace Tickfy.Persistence.EntitiesConfigurations;

public class FlightReservationConfigurations : IEntityTypeConfiguration<FlightReservation>
{
    public void Configure(EntityTypeBuilder<FlightReservation> builder)
    {
        builder.HasOne(fr => fr.Flight)
            .WithMany(f => f.Reservations)
            .HasForeignKey(fr => fr.FlightId)
            .OnDelete(DeleteBehavior.Restrict); 

        builder.HasOne(fr => fr.Class)
            .WithMany(c => c.Reservations)
            .HasForeignKey(fr => fr.ClassId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(fr => fr.Status)
            .HasConversion<String>()
            .HasMaxLength(50);
    }
}