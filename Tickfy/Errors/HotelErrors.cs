namespace Tickfy.Errors;

public static class HotelErrors
{
    public static readonly Error NoAnyHotel =
        new Error(" No Any Hotel Found", "No Hotel was found.");
    public static readonly Error HotelNotFound =
        new Error("Hotel.NotFound", "No Hotel was found with the given ID.");
    public static readonly Error EmptyHotelResults =
        new Error("Empty Results.", "There is no Hotels matches your criteria.");
    public static readonly Error InvalidStarRating =
         new Error("Hotel.InvalidStarRating", "The provided star rating is not valid.");
    public static readonly Error NoRoomsForHotel =
        new Error("NO_ROOMS_FOR_HOTEL", "This hotel does not have any assigned rooms.");
}
