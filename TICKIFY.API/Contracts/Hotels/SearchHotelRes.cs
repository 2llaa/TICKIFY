using TICKIFY.API.Contracts.Drivers;
using TICKIFY.API.Contracts.Rooms;
using TICKIFY.Data.Enums;

namespace TICKIFY.API.Contracts.hotels
{
    public class SearchHotelRes
    {
        public int HotelId { get; set; } 
        public string Name { get; set; } = string.Empty; 
        public string Location { get; set; } = string.Empty; 
        public int StarRating { get; set; } 
        public List<HotelDriversRes> Drivers { get; set; } = new List<HotelDriversRes>();
        public List<HotelRoomsRes> Avaliable_Rooms { get; set; } = new List<HotelRoomsRes>(); 
    }
}
