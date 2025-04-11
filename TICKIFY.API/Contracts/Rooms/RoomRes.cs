using TICKIFY.Data.Enums;

namespace TICKIFY.API.Contracts.Rooms
{
    public class RoomRes
    {
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }
        public string Type { get; set; }
        public int BedCount { get; set; }
        public decimal PricePerNight { get; set; }
        public string Status { get; set; }  
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }
        public int HotelId { get; set; }

        public string HotelName { get; set; } = string.Empty; 

    }
}