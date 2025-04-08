using TICKIFY.API.Contracts.Drivers;
using TICKIFY.API.Contracts.hotels;

namespace TICKIFY.API.Contracts.HotelReservations
{
    public class HotelReservationRes
    {
        public int HotelReservationId { get; set; }
        public int HotelId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public List<HotelDriverRes> Details { get; set; } = new List<HotelDriverRes>();


    }
}
