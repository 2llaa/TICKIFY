using TICKIFY.Data.Enums;

namespace TICKIFY.API.Contracts.Rooms
{
    public class HotelRoomsRes
    {
        public int RoomId { get; set; }
        public string Status { get; set; }  // حالة الغرفة (متاحة، محجوزة، إلخ)
        public RoomType Type { get; set; }
        public int RoomNumber { get; set; } 
        public int BedCount { get; set; }   
        public decimal PricePerNight { get; set; } 
  
    }
}
