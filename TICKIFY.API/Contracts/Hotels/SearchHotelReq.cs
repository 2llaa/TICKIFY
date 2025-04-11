using TICKIFY.Data.Enums;

namespace TICKIFY.API.Contracts.hotels
{
    public class SearchHotelReq
    {
        public string? Name { get; set; } 
        public string? Location { get; set; } 
        public int? StarRating { get; set; } 


    }
}
