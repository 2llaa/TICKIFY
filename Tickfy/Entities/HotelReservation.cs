using Tickfy.Enums;

namespace Tickfy.Entities;

public class HotelReservation:AuditableEntity
{
    public int Id { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public ReservationStatus Status { get; set; } = ReservationStatus.Confirmed;
    public decimal TotaPrice { get; set; }
    public int HotelId { get; set; }

    public Hotel Hotel { get; set; } = default!;


}
