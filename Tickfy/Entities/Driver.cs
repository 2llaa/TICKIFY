using Tickfy.Enums;

namespace Tickfy.Entities
{
    public class Driver
    {
        public int DriverId { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public DriverCarType CarType { get; set; } = DriverCarType.None;
        public decimal Price { get; set; }
        public int StarRating { get; set; }

    }
}
