namespace TICKIFY.API.Contracts.Rooms
{
    public class RoomDto2
    {
        public int RoomNumber { get; set; }
        public string Type { get; set; }
        public int BedCount { get; set; }
        public decimal PricePerNight { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }
        public string Status { get; set; }
    }
}
