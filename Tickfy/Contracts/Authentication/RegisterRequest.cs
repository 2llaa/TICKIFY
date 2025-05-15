namespace Tickfy.Contracts.Authentication;

public record RegisterRequest(
    String Email,
    String Password,
    String FirstName,
    String LastName
);