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

            builder.Property(hr => hr.HotelId)
                .IsRequired();

            builder.Property(hr => hr.DriverId)
                .IsRequired();

            builder.Property(hr => hr.RoomId)
                .IsRequired();

            // Relationships
            builder.HasOne(hr => hr.Driver)
                .WithMany(h => h.HotelReservations)
                .HasForeignKey(hr => hr.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(hr => hr.Hotel)
                .WithMany(h => h.HotelReservations)
                .HasForeignKey(hr => hr.HotelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(hr => hr.Room)
                .WithMany(r => r.HotelReservations)
                .HasForeignKey(hr => hr.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            // Updated to match ReservationDetails configuration
            builder.HasMany(hr => hr.ReservationDetails)
                 .WithOne(rd => rd.HotelReservation)
                 .HasForeignKey(rd => rd.HotelReservationId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict); // Changed to Restrict


            // Soft delete filter
            builder.HasQueryFilter(r => !r.IsDeleted);
        }
    }
}