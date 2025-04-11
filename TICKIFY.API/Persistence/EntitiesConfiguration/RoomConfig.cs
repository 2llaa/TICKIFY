using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TICKIFY.Data.Entities;
using TICKIFY.Data.Enums;

namespace TICKIFY.Infrastracture.Persistence.EntitiesConfiguration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Rooms>
    {
        public void Configure(EntityTypeBuilder<Rooms> builder)
        {
            builder.HasKey(r => r.RoomId);

            builder.Property(r => r.Type)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Status)
                .IsRequired();

            builder.Property(r => r.BedCount)
                .IsRequired();

            builder.Property(r => r.PricePerNight)
                .HasColumnType("decimal(10,2)");

            builder.Property(r => r.DateIn)
                .IsRequired();

            builder.Property(r => r.DateOut)
                .IsRequired();

            builder.HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);  

            builder.HasMany(r => r.HotelReservations)
                .WithOne(hr => hr.Room)
                .HasForeignKey(hr => hr.RoomId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasMany(r => r.ReservationDetails)
                .WithOne(rd => rd.Room)
                .HasForeignKey(rd => rd.RoomId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
