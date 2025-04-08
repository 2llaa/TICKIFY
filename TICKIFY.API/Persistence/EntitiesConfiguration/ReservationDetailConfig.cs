using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TICKIFY.Data.Entities;

namespace TICKIFY.Infrastracture.Persistence.EntitiesConfiguration
{
    public class ReservationDetailConfig : IEntityTypeConfiguration<ReservationDetails>
    {
        public void Configure(EntityTypeBuilder<ReservationDetails> builder)
        {
            builder.HasKey(rd => rd.ReservationDetailsId);

            builder.Property(rd => rd.HotelReservationId)
                .IsRequired();

            builder.Property(rd => rd.RoomId)
                .IsRequired();

            builder.HasOne(rd => rd.HotelReservation)
                .WithMany(hr => hr.ReservationDetails)
                .HasForeignKey(rd => rd.HotelReservationId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(rd => rd.Room)
                .WithMany(r => r.ReservationDetails)
                .HasForeignKey(rd => rd.RoomId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
