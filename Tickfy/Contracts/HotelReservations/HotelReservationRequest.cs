namespace Tickfy.Contracts.HotelReservations;

public record HotelReservationRequest(
    IEnumerable<int>RoomsId,
    DateTime CheckInDate,
    DateTime CheckOutDate
);

     

