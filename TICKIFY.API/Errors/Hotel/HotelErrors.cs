using TICKIFY.API.Abstracts;

namespace TICKIFY.API.Errors.Hotel
{
    public class HotelErrors:Error
    {
        public HotelErrors(string code, string description) : base(code, description) { }

        public static readonly Error HotelNotFound = new("HOTEL_NOT_FOUND", "The specified hotel could not be found.");

        public static readonly Error DuplicateHotel = new("DUPLICATE_HOTEL", "A hotel with the same name already exists.");
        public static readonly Error HotelNot = new("INVALID_HOTEL_type", "The provided hotel type is invalid.");
        public static readonly Error InvalidHotelName = new("INVALID_HOTEL_Name", "The provided hotel already exist.");
        public static readonly Error NoHotelsFound = new("NO_HOTELS_FOUND", "There are no hotels available.");

        public static readonly Error NoRoomsForHotel = new("NO_ROOMS_FOR_HOTEL", "This hotel does not have any assigned rooms.");
        public static readonly Error Not = new Error("NOT", "This hotel does not have any assigned ");
        
        public static readonly Error NoDriversForHotel = new("NO_DRIVERS_FOR_HOTEL", "This hotel does not have any assigned drivers.");

    }
}
