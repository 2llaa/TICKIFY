using TICKIFY.API.Contracts.Drivers;
using TICKIFY.API.Contracts.Rooms;

namespace TICKIFY.API.Contracts.Hotels
{
    public class HotelByIdRes
    {
        public int HotelId { get; set; } 

        public string Name { get; set; } = string.Empty; 


        public string Location { get; set; } = string.Empty; 

        public int StarRating { get; set; } 

        public List<HotelDriversRes> Drivers { get; set; } = new List<HotelDriversRes>();
        public List<HotelByIdRoomRes> Hotel_Rooms { get; set; } = new List<HotelByIdRoomRes>(); 
    }
}
