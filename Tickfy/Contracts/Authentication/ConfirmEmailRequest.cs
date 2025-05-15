namespace Tickfy.Contracts.Authentication
{
    public record ConfirmEmailRequest(
        String UserId,
        String Code
     );
}
