using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TICKIFY.Data.Entities;

namespace TICKIFY.Infrastructure.Persistence.EntitiesConfiguration
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotels>
    {
        public void Configure(EntityTypeBuilder<Hotels> builder)
        {
            builder.HasKey(h => h.HotelId);

            builder.Property(h => h.Location)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(h => h.StarRating)
                   .IsRequired();

            builder.HasMany(h => h.Rooms)
                   .WithOne(r => r.Hotel)
                   .HasForeignKey(r => r.HotelId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(h => h.Drivers)
                   .WithOne(d => d.Hotel)
                   .HasForeignKey(d => d.HotelId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
