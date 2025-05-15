using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tickfy.Entities;

namespace Tickfy.Persistence.EntitiesConfigurations;

public class FlightConfigurations : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.Property(x => x.ArrivalAirport).IsRequired();
        builder.Property(x => x.ArrivalAirport).HasMaxLength(100);
        builder.Property(x => x.DepartureAirport).HasMaxLength(100);

        
    }
}
