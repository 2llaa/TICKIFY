namespace Tickfy.Contracts.Rooms;

public record SelectRoomRequest(
    int RoomType,
    int? BedType,
    int? OpptionalBedType,
    DateTime CheckInDate,
    DateTime CheckOutDate
);

