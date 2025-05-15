using Microsoft.AspNetCore.Identity;

namespace Tickfy.Entities;

public class ApplicationUser:IdentityUser
{
    public String FirstName { get; set; } = string.Empty;
    public String LastName { get; set; } = string.Empty;

   
}
