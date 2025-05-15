using Tickfy.Contracts.Classes;

namespace Tickfy.Contracts.Flights;

public record SearchFlightResponse(
    int Id,
    DateTime DepartureDate,
    DateTime ArrivalDate,
    string DepartureAirport,
    string ArrivalAirport,
    string Airport,
    IEnumerable<ClassResponse>Classes
);
