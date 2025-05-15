using FluentValidation;
using System.Data;

namespace Tickfy.Contracts.Flights;

public class SearchFlightRequestValidator : AbstractValidator<SearchFlightRequest>
{
    public SearchFlightRequestValidator()
    {
        Console.WriteLine("🚀 FlightRequestValidator is being loaded!");

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
            .WithMessage("{PropertyName} invalid * must be after now * ");

        RuleFor(x => x.ArrivalDate)
           .NotEmpty()
           .WithMessage("{PropertyName} is Required");

        RuleFor(x => x)
            .Must(HasValiDate)
            .WithName(nameof(SearchFlightRequest.ArrivalDate))
            .WithMessage("{PropertyName} must be greater than Departure date}");

    }

    private bool HasValiDate(SearchFlightRequest flightRequest)
    {
        return flightRequest.ArrivalDate > flightRequest.DepartureDate;
    }

}
