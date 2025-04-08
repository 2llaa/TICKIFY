using TICKIFY.Data.Enums;

namespace TICKIFY.API.Contracts.Rooms
{
    public class RoomReq
    {
        public int RoomNumber { get; set; }
        public RoomType Type { get; set; }
        public decimal PricePerNight { get; set; }
        public int HotelId { get; set; }
    }
}