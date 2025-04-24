using Microsoft.AspNetCore.Identity;

namespace TICKIFY.API.Persistence.DataSeed
{
    public class SeedRoles
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(role));
                    Console.WriteLine($"Created role: {role}, Success: {result.Succeeded}");
                }
                else
                {
                    Console.WriteLine($"Role already exists: {role}");
                }
            }
        }
    }
}