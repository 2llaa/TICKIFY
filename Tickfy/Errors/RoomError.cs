namespace Tickfy.Errors;

public class RoomError
{
  
    public static readonly Error RoomNotFound =
       new Error("Room.NotFound", "No Room was found with the given ID.");
    public static readonly Error EmptyReservationsResults =
        new Error("Empty Results.", "There is no Reservations for this Room.");

}
