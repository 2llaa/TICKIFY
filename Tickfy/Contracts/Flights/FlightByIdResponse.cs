using Tickfy.Contracts.Classes;

namespace Tickfy.Contracts.Flights;

public record FlightByIdResponse(
    DateTime DepartureDate,
    DateTime ArrivalDate,
    string DepartureAirport,
    string ArrivalAirport,
    string Airport,
    bool IsActive,
    IEnumerable<ClassResponse> Classes
);
