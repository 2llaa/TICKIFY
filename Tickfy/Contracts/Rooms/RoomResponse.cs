using Tickfy.Contracts.BedInfos;
using Tickfy.Enums;

namespace Tickfy.Contracts.Rooms;

public record RoomResponse(
   int Id,
   RoomType RoomType,
   //int RoomNumber,
   decimal PricePerNight,
   IEnumerable<BedInfoResponse>Beds
);
