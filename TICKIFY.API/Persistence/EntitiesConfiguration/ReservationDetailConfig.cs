using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TICKIFY.Data.Entities;

namespace TICKIFY.Infrastructure.Persistence.EntitiesConfiguration
{
    public class ReservationDetailConfig : IEntityTypeConfiguration<ReservationDetails>
    {
        public void Configure(EntityTypeBuilder<ReservationDetails> builder)
        {
            builder.HasKey(rd => rd.ReservationDetailsId);

            // Keep as required
            builder.Property(rd => rd.HotelReservationId)
                .IsRequired();

            builder.HasOne(rd => rd.HotelReservation)
                .WithMany(hr => hr.ReservationDetails)
                .HasForeignKey(rd => rd.HotelReservationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); // Changed to Restrict

            builder.HasOne(rd => rd.Room)
                .WithMany(r => r.ReservationDetails)
                .HasForeignKey(rd => rd.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(rd => !rd.IsDeleted);
        }
    }
}