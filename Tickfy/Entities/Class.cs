using Tickfy.Enums;

namespace Tickfy.Entities;

public class Class
{
    public int Id { get; set; }
    public String className { get; set; } = String.Empty;
    public decimal Price { get; set; }
    public int Capacity { get; set; } = 0;
    public int AvailableSeats { get; set; } = 0;
    public int FlightId { get; set; }
    public bool IsActive { get; set; } = true;

    public Flight Flight { get; set; } = default!;
    public ICollection<FlightReservation> Reservations { get; set; } = [];


}
