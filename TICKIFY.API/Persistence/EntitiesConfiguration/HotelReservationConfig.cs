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

            builder.Property(hr => hr.HotelId)
                .IsRequired(); 

            builder.HasOne(hr => hr.Hotel)
                .WithMany(h => h.HotelReservations)
                .HasForeignKey(hr => hr.HotelId) 
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
