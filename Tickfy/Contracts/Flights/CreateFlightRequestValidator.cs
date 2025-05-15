using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Tickfy.Persistence;
using Tickfy.Services.Flights;

namespace Tickfy.Contracts.Flights;

public class CreateFlightRequestValidator:AbstractValidator<CreateFlightRequest>
{

    public CreateFlightRequestValidator()

    {

        RuleFor(x => x.DepartureAirport)
            .NotEmpty()
            .WithMessage("{PropertyName} must be not empty")
            .MaximumLength(100)
            .WithMessage("{PropertyName} Has maximum Length {MaxLength}");

        RuleFor(x => x.ArrivalAirport)
           .NotEmpty()
           .WithMessage("{PropertyName} must be not empty")
           .MaximumLength(3)
           .WithMessage("{PropertyName} Has maximum Length {MaxLength}");


        RuleFor(x => x.DepartureDate)
            .NotEmpty()
            .WithMessage("{PropertyName} is Required")
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("{PropertyName} must be in the future");

        RuleFor(x => x.ArrivalDate)
           .NotEmpty()
           .WithMessage("{PropertyName} is Required");

        RuleFor(x => x.Airport)
          .NotEmpty()
          .WithMessage("{PropertyName} is required");


        RuleFor(x => x)
            .Must(HasValiDate)
            .WithName(nameof(CreateFlightRequest.ArrivalDate))
            .WithMessage("{PropertyName} must be greater than DepartureDate");

      

    }

    private bool HasValiDate(CreateFlightRequest flightRequest)
    {
        return flightRequest.ArrivalDate > flightRequest.DepartureDate;
    }

   
}
