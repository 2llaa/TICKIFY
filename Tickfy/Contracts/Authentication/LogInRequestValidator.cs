using FluentValidation;
using Tickfy.Contracts.FlightReservation;

namespace Tickfy.Contracts.Authentication;

public class LogInRequestValidator : AbstractValidator<LogInRequest>
{
    public LogInRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
           .NotEmpty();

    }
}
