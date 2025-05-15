using Tickfy.Contracts.Rooms;
using Tickfy.Enums;

namespace Tickfy.Contracts.Hotels;

public record HotelResponse(
     int Id,
     int StarRating,
     string Name,
     //string Location, 
     //string Description, 
     IEnumerable<RoomResponse> Rooms
);

