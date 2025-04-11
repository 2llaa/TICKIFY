using TICKIFY.API.Contracts.Drivers;
using TICKIFY.Data.Enums;

namespace TICKIFY.API.Contracts.hotels
{
    public class HotelRes
    {
        public int HotelId { get; set; } 

        public string Name { get; set; } = string.Empty; 

        public string Location { get; set; } = string.Empty; 

        public int StarRating { get; set; } 

        public List<HotelDriversRes> Drivers { get; set; } = new List<HotelDriversRes>();
    }
}
