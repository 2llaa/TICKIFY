namespace TICKIFY.API.Contracts.Drivers
{
    public class HotelDriversRes
    {
        public int DriverId { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public string CarType { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StarRating { get; set; }
    }
}
