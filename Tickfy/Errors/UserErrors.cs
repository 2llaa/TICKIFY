
using Tickfy.Abstractions;

namespace Tickfy.Errors;

public static class UserErrors
{
    public static readonly Error InvalidCredentials =
        new("InvalidCredentials", "Invalid Email/Password.");
    public static readonly Error DuplicatedEmail =
        new("DuplicatedEmail", "Another user with same email is already exist");
    public static readonly Error EmailNotConfaimed =
       new("EmailNotConfaimed", "Email is Not Confirmed.");
    public static readonly Error InvalidCode =
       new("InvalidCode", "Invalid Code .");
    public static readonly Error DuplicatedConfirmation =
       new("DuplicatedConfirmation", "email is already Confirmed.");
}