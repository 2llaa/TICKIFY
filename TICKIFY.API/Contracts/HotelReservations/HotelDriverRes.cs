namespace TICKIFY.API.Contracts.HotelReservations
{
    public class HotelDriverRes
    {
        public string HotelName { get; set; } = string.Empty;
        public string RoomType { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public string CarType { get; set; } = string.Empty;
    }
}
