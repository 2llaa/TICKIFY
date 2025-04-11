namespace TICKIFY.API.Contracts.HotelReservations
{
    public class HotelReservationReq
    {
        public string Email { get; set; }
        public string GuestName { get; set; }
        public int RoomId { get; set; }
        public string Phone { get; set; }
        public DateTime CheckOutData { get; set; }
    }
}
