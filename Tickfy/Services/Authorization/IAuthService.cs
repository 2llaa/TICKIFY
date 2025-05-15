using Tickfy.Abstractions;
using Tickfy.Contracts.Authentication;

namespace Tickfy.Services.Authorization;

public interface IAuthService
{
    Task<Result<AuthResponse>> GetTokenAsync(String email, String password, CancellationToken cancellationToken = default!);
    Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default!);
    Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request);
    Task<Result> ResendConfirmationEmailAsync(ResendConfirmationEmail request);
}