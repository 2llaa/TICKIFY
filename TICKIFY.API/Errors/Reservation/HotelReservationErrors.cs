using TICKIFY.API.Abstracts;

namespace TICKIFY.API.Errors.Reservation
{
    public class HotelReservationErrors
    {
        public static readonly Error ReservationNotFound =new("RESERVATION_NOT_FOUND", "The requested reservation could not be found.");
        public static readonly Error NoReservationsFound =new("NO_RESERVATIONS_FOUND", "There are no reservations at the moment.");
        public static readonly Error InvalidReservationStatus = new("INVALID_RESERVATION_STATUS", "The reservation status is not valid.");
        public static readonly Error ReservationAlreadyExists = new("RESERVATION_ALREADY_EXISTS", "A reservation with the same details already exists.");
        public static readonly Error ReservationDateInvalid = new("RESERVATION_DATE_INVALID", "The reservation date is invalid.");
        public static readonly Error RoomNotAvailable = new("ROOM_NOT_AVAILABLE", "The requested room is not available for the selected dates.");
        public static readonly Error RoomAlreadyBooked = new("ROOM_ALREADY_BOOKED", "The room is already booked for the selected dates.");

    }
}
