using System.Text.Json.Serialization;
using TICKIFY.Data.Enums;

namespace TICKIFY.Data.Entities
{
    public class Hotels
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; } = string.Empty;
        public int StarRating { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        [JsonIgnore]
        public List<HotelReservations> HotelReservations { get; set; } = [];

        public List<Drivers> Drivers { get; set; } = [];
        [JsonIgnore]

        public List<Rooms> Rooms { get; set; } = [];
        public string Status { get; set; }
    }
}