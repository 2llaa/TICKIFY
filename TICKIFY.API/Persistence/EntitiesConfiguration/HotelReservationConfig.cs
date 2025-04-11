using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TICKIFY.Data.Entities;

namespace TICKIFY.Infrastructure.Persistence.EntitiesConfiguration
{
    public class HotelReservationConfig : IEntityTypeConfiguration<HotelReservations>
    {
        public void Configure(EntityTypeBuilder<HotelReservations> builder)
        {
            builder.HasKey(hr => hr.HotelReservationId);

            builder.Property(hr => hr.CheckInDate)
                .IsRequired();

            builder.Property(hr => hr.CheckOutDate)
                .IsRequired();

            builder.Property(hr => hr.Status)
                .IsRequired();

            builder.Property(hr => hr.GuestName)
                .IsRequired()
                .HasMaxLength(255); 

            builder.Property(hr => hr.Email)
                .IsRequired()
                .HasMaxLength(255); 

            builder.Property(hr => hr.Phone)
                .IsRequired()
                .HasMaxLength(20); 

            builder.Property(hr => hr.HotelId)
                .IsRequired();

            builder.HasOne(hr => hr.Hotel)
                .WithMany(h => h.HotelReservations)
                .HasForeignKey(hr => hr.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(hr => hr.RoomId)
                .IsRequired();

            builder.HasOne(hr => hr.Room)
                .WithMany(r => r.HotelReservations)
                .HasForeignKey(hr => hr.RoomId)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.HasMany(hr => hr.ReservationDetails)
                .WithOne(rd => rd.HotelReservation)
                .HasForeignKey(rd => rd.HotelReservationId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
