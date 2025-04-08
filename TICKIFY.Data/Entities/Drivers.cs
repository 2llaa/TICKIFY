using System.Text.Json.Serialization;
using TICKIFY.Data.Enums;

namespace TICKIFY.Data.Entities
{
    public class Drivers
    {
        public int DriverId { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public DriverCarType CarType { get; set; } = DriverCarType.None;
        public decimal Price { get; set; }
        public int StarRating { get; set; }

        public int HotelId { get; set; }
        public Hotels Hotel { get; set; }


    }
}