namespace Tickfy.Contracts.FlightReservation;

public record FlightReservationResponse(
    String CustomerName,
    String CustomerEmail,
    String CustomerPhone,
    DateTime CreatedOn,
    String DepartureAirport,
    String ArrivalAirport,  
    DateTime DepartureDate,
    DateTime ArrivalDate,
    int SeatNumber,
    String ClassName,
    decimal Price
);
