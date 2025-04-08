using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TICKIFY.API.Persistence.Data;

namespace TICKIFY.API
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // حطي هنا نفس الكونكشن سترينج اللي في appsettings.json
            var connectionString = "Server=.;Database=TickifyDb;Trusted_Connection=True;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
