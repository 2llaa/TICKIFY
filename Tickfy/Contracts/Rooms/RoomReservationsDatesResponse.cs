namespace Tickfy.Contracts.Rooms;

public record RoomReservationsDatesResponse(
    DateTime CheckInDate,
    DateTime CheckOutDate
);

