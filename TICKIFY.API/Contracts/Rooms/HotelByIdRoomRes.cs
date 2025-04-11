namespace TICKIFY.API.Contracts.Rooms
{
    public class HotelByIdRoomRes
    {
        public int RoomId { get; set; }
         public string Status { get; set; }  
        public string Type { get; set; }
        public int RoomNumber { get; set; }
        public int BedCount { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
