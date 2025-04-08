using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TICKIFY.Data.Entities;

namespace TICKIFY.Infrastracture.Persistence.EntitiesConfiguration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Rooms>
    {
        public void Configure(EntityTypeBuilder<Rooms> builder)
        {
            builder.HasKey(r => r.RoomId);
            builder.Property(r => r.Type).IsRequired().HasMaxLength(100);
            builder.Property(r => r.BedCount).IsRequired();
            builder.Property(r => r.PricePerNight).HasColumnType("decimal(10,2)");
            builder.Property(r => r.DateIn).IsRequired();
            builder.Property(r => r.DateOut).IsRequired();

            builder.HasOne(r => r.Hotel)
                   .WithMany(h => h.Rooms)
                   .HasForeignKey(r => r.HotelId)
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
