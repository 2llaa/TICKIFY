using System.Text.Json.Serialization;

namespace TICKIFY.Data.Entities
{
    public class ReservationDetails
    {
        public int ReservationDetailsId { get; set; }
        public int RoomId { get; set; }

        [JsonIgnore]
        public Rooms Room { get; set; }

        public int HotelReservationId { get; set; } 

        [JsonIgnore]
        public HotelReservations HotelReservation { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
    }
}