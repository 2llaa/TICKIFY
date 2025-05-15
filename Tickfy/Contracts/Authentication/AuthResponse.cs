namespace Tickfy.Contracts.Authentication;

public record AuthResponse(

     String Id,
     String? Email,
     String FirstName,
     String LastName,
     String Token,
     int ExpiresIn
);
