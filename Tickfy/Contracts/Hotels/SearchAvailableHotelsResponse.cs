using Tickfy.Contracts.Rooms;

namespace Tickfy.Contracts.Hotels;

public record SearchAvailableHotelsResponse(
    int Id,
    String Name,
    IEnumerable<RoomResponse>Rooms
);

