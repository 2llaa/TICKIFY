using System.Security.Principal;

namespace TICKIFY.API.Contracts.Auth
{
    public class AuthRes
    {
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }
        public DateTime ExpirationDate { get; set; }

    }
}
