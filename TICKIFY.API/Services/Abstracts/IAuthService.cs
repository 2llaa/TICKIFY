using TICKIFY.API.Abstracts;
using TICKIFY.API.Contracts.Auth;
using TICKIFY.API.Contracts.Logging;

namespace TICKIFY.API.Services.Abstracts
{
    public interface IAuthService
    {

        Task<AuthRes> RegisterAsync(RegisterReq model);
        Task<AuthRes> GetTokenAsync(LoginReq model);
        Task<string> AddRoleAsync(AssignRoleReq model);
    }
}
