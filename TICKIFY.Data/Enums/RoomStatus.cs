using System.Runtime.Serialization;

namespace TICKIFY.Data.Enums
{
    public enum RoomStatus
    {
        [EnumMember(Value = "Available")]

        Available,
        [EnumMember(Value = "Reserved")]

        Reserved,
        [EnumMember(Value = "Occupied")]

        Occupied
    }
}
