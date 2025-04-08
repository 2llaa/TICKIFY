using TICKIFY.Data.Enums;

namespace TICKIFY.API.Contracts.Requests
{
    public class DriverReq
    {

        public string DriverName { get; set; } = string.Empty;
        public string CarType { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StarRating { get; set; } = 5;
        public int HotelId { get; set; }
    }
}

