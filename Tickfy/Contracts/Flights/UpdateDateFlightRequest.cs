namespace Tickfy.Contracts.Flights;

public record UpdateDateFlightRequest(
    int Id,
    DateTime DepartureDate,
    DateTime ArrivalDate
);
