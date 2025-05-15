
namespace Tickfy.Persistence.EntitiesConfigurations;

public class ClassConfigurations : IEntityTypeConfiguration<Class>
{
    public void Configure(EntityTypeBuilder<Class> builder)
    {
        builder.Property(c => c.className)
            .HasMaxLength(50)
            .IsRequired()
            .HasConversion<String>();

        builder.Property(c => c.Price)
          .HasColumnType("decimal(18, 2)")
          .IsRequired();

        builder.Property(c => c.Capacity)
            .IsRequired();

        builder.HasOne(f => f.Flight)
           .WithMany(c => c.Classes)
           .HasForeignKey(fr => fr.FlightId)
           .OnDelete(DeleteBehavior.Restrict);

      

    }
}
