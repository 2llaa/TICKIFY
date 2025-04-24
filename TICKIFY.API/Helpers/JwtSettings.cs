using System.ComponentModel.DataAnnotations;

namespace TICKIFY.API.Helpers
{
    public class JwtSettings
    {
        [Required]
        public string SecretKey { get; set; } = string.Empty;

        [Required]
        public string Issuer { get; set; } = string.Empty;

        [Required]
        public string Audience { get; set; } = string.Empty;

        [Range(1, 365)]
        public int DurationInDays { get; set; }
    }
}
