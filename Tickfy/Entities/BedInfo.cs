using System.Text.Json.Serialization;
using Tickfy.Enums;

namespace Tickfy.Entities;

public class BedInfo
{
    public int Id { get; set; }    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public BedType Type { get; set; }
    public int Count { get; set; }
    public int RoomId { get; set; }



}
