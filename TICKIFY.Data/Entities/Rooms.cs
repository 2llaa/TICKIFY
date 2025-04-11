using System.Text.Json.Serialization;
using TICKIFY.Data.Enums;

namespace TICKIFY.Data.Entities
{
    public class Rooms
    {
        public int RoomId { get; set; }

        public RoomType Type { get; set; }
        public string Status { get; set; } =string.Empty; // حالة الغرفة (متاحة، محجوزة، إلخ)
        public int RoomNumber { get; set; }
        public int BedCount { get; set; }
        public decimal PricePerNight { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }
        public int HotelId { get; set; }

        // public bool IsAvailable { get; set; }
        [JsonIgnore]
        public Hotels Hotel { get; set; }
        [JsonIgnore]
        public List<ReservationDetails> ReservationDetails { get; set; } = [];
        [JsonIgnore]
        public List<HotelReservations> HotelReservations { get; set; } = [];
    }
}
