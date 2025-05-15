using System.Text.Json.Serialization;

namespace Tickfy.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ClassType
{
    Economy,
    Business,
    FirstClass,
    VIP
}