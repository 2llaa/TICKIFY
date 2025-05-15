using Tickfy.Entities;

namespace Tickfy.Authentication;

public interface IJwtProvider
{
    (String token, int expiresIn) GeneratedToken(ApplicationUser user);
}
