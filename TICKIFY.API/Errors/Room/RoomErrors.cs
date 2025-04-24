using TICKIFY.API.Abstracts;

namespace TICKIFY.API.Errors.Room
{
    public class RoomErrors
    {
        public RoomErrors(string code, string description)
        { }


        public static readonly Error RoomNotFound =
            new("ROOM_NOT_FOUND", "The specified room could not be found.");

        public static readonly Error RoomStatusNotFound =
            new("ROOM_STATUS_NOT_FOUND", "No rooms found with the given status.");

        public static readonly Error RoomTypeNotFound =
            new("ROOM_TYPE_NOT_FOUND", "No rooms found with the given type.");

        public static readonly Error NoRoomsFound =
            new("NO_ROOMS_FOUND", "There are no rooms available.");

        public static readonly Error NoRoomsForHotel =
            new("NO_ROOMS_FOR_HOTEL", "No rooms were found for the selected hotel.");

        public static Error DatabaseOperationFailed =
            new("DATABASE_OPERATION_FAILED", "An error occurred while accessing the database.");

        public static Error UnknownError =
            new("UNKNOWN_ERROR", "An unknown error occurred.");
        public static Error RoomHasActiveReservations =
            new("ROOM_HAS_ACTIVE_RESERVATIONS", "The room has active reservations and cannot be deleted.");
        public static Error NoAvailableRooms =
            new("NO_AVAILABLE_ROOMS", "No available rooms found for the specified criteria.");
        public static Error DuplicateRoomNumber =
            new("DUPLICATE_ROOM_NUMBER", "A room with the same number already exists in the hotel.");
    }
}
