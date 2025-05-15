using System.ComponentModel.DataAnnotations;

namespace Tickfy.Authentication;

public class JwtOptions
{
    public static string SectionName = "Jwt";

    [Required]
    public string Key { get; set; } = string.Empty;

    [Required]
    public string Issuer { get; set; } = string.Empty;

    [Required]
    public string Audience { get; set; } = string.Empty;

    [Range(100,1000)]
    public int ExpirationMinutes { get; set; }

}
