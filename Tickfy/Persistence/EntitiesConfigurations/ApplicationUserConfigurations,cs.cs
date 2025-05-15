using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tickfy.Entities;

namespace Tickfy.Persistence.EntitiesConfigurations;

public class ApplicationUserConfigurations_cs : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(a => a.FirstName)
            .HasMaxLength(50);
        builder.Property(a => a.LastName)
           .HasMaxLength(50);
    }
}
