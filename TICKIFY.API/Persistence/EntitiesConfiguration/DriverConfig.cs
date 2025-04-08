using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TICKIFY.Data.Entities;

public class DriverConfiguration : IEntityTypeConfiguration<Drivers>
{
    public void Configure(EntityTypeBuilder<Drivers> builder)
    {
        builder.HasKey(d => d.DriverId);

        builder.Property(d => d.DriverName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(d => d.CarType)
               .IsRequired()
               .HasMaxLength(50); 

        builder.Property(d => d.Price)
               .HasColumnType("decimal(18,2)");

        builder.Property(d => d.StarRating)
               .IsRequired()
               .HasDefaultValue(5);

        builder.HasOne(d => d.Hotel) 
               .WithMany(h => h.Drivers)
               .HasForeignKey(d => d.HotelId) 
               .OnDelete(DeleteBehavior.Cascade); 
    }
}
