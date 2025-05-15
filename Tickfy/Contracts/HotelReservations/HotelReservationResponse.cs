namespace Tickfy.Contracts.HotelReservations;

public record HotelReservationResponse(
  
    int Id,
    String CustomerName,
    String CustomerEmail,
    String CustomerPhone,
    DateTime CreatedOn,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    decimal TotalPrice
);
