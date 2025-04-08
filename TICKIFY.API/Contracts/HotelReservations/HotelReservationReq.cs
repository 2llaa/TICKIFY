namespace TICKIFY.API.Contracts.HotelReservations
{
    public class HotelReservationReq
    {
        public int HotelId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
