using System.Text.Json.Serialization;
using Tickfy.Enums;

namespace Tickfy.Entities;

public class Room
{
    public int Id { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public RoomType RoomType { get; set; }
    public decimal PricePerNight { get; set; }
    public bool IsAvailable { get; set; } = true;
    public ICollection<BedInfo> Beds { get; set; } = [];
    
    public ICollection<HotelReservationRoom> RoomReservations { get; set; } = [];

    public int HotelId { get; set; }
    public Hotel Hotel { get; set; } = default!;


}
