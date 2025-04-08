using TICKIFY.API.Contracts.Drivers;
using TICKIFY.Data.Enums;

namespace TICKIFY.API.Contracts.Requests
{
    public class HotelReq
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int StarRating { get; set; }
        public HotelCategory Category { get; set; }

    }
}
