using Tickfy.Entities;

namespace Tickfy.Persistence.EntitiesConfigurations
{
    public class DriverConfigurations : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
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

        }
    }
}
