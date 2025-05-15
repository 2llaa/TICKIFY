using FluentValidation;
using System.Data;

namespace Tickfy.Contracts.FlightReservation;

public class FlightReservationRequestValidator : AbstractValidator<FlightReservationRequest>
{
    public FlightReservationRequestValidator()
    {

    }

}
