using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tickfy.Contracts.Authentication;
using Tickfy.Services.Authorization;

namespace Tickfy.Controllers;
[Route("[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("LogIn")]
    public async Task<IActionResult> LogIn(LogInRequest logInRequest, CancellationToken cancellationToken)
    {
        var authResult = await _authService.GetTokenAsync(logInRequest.Email, logInRequest.Password, cancellationToken);

        return authResult.IsSuccess ? Ok(authResult.Value) : BadRequest(authResult!.Error);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.RegisterAsync(request, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result!.Error);
    }

    [HttpPost(template: "confirm-email")]
    public async Task<IActionResult> ConfirmEmail(ConfirmEmailRequest request)
    {
        var result = await _authService.ConfirmEmailAsync(request);

        return result.IsSuccess ? Ok() : BadRequest(result!.Error);
    }

    [HttpPost(template: "resend-confirmation-email")]
    public async Task<IActionResult> ResendConfirmationEmail(ResendConfirmationEmail request)
    {
        var result = await _authService.ResendConfirmationEmailAsync(request);

        return result.IsSuccess ? Ok() : BadRequest(result!.Error);
    }
}