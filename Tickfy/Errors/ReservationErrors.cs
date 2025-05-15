namespace Tickfy.Errors;

public static class ReservationErrors
{
    public static readonly Error NoAnyReservation =
        new Error(" No Any Reservation Found", "No Reservation was found.");
    public static readonly Error ReservationNotFound =
        new Error("Reservation.NotFound", "No Reservation was found with the given ID.");
    public static readonly Error EmptyReservationsResults =
        new Error("Empty Results.", "There is no Reservations matches your criteria.");
    public static Error NotValidRooms(string message) =>
       new Error("NotValidRooms", message + "\n" + "These rooms are already reserved at the same time.");
}