using Tickfy.Enums;

namespace Tickfy.Entities;

public class FlightReservation: AuditableEntity
{
    public int Id { get; set; }
    public int SeatNumber { get; set; }
    public ReservationStatus Status { get; set; } = ReservationStatus.Confirmed;
    public int FlightId { get; set; }
    public int ClassId { get; set; }


    public Flight Flight { get; set; } = default!;
    public Class Class { get; set; } = default!;

}
