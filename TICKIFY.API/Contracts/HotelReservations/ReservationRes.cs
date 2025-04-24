namespace TICKIFY.API.Contracts.HotelReservations
{
    public class ReservationRes
    {
        public int ReservationId { get; set; }
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public int RoomNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public string DriverName { get; set; }
        public int RoomId { get; set; }
        public int DriverId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; }
    }
}
