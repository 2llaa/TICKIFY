namespace Tickfy.Contracts.Flights;

public record SearchFlightRequest(
    DateTime DepartureDate,
    DateTime ArrivalDate,
    string DepartureAirport,
    string ArrivalAirport
);
