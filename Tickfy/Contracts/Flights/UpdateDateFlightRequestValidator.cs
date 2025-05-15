using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading;
using Tickfy.Persistence;

namespace Tickfy.Contracts.Flights;

public class UpdateDateFlightRequestValidator : AbstractValidator<UpdateDateFlightRequest>
{

    public UpdateDateFlightRequestValidator( )
    {



        RuleFor(x => x.DepartureDate)
            .NotEmpty()
            .WithMessage("{PropertyName} is Required")
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("{PropertyName} must be in the future");

        RuleFor(x => x.ArrivalDate)
           .NotEmpty()
           .WithMessage("{PropertyName} is Required");

        RuleFor(x => x)
            .Must(HasValidDate)
            .WithName(nameof(UpdateDateFlightRequest.ArrivalDate))
            .WithMessage("{PropertyName} must be greater than Departure date");
    }


    private bool HasValidDate(UpdateDateFlightRequest flightRequest)
    {
        return flightRequest.ArrivalDate > flightRequest.DepartureDate;
    }



}
