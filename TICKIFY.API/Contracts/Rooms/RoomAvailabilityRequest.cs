namespace TICKIFY.API.Contracts.Rooms
{
    public class RoomAvailabilityRequest
    {
        public int HotelId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
