namespace Tickfy.Entities;

public class Flight
{
    public int Id { get; set; }
    public DateTime DepartureDate { get; set; } = DateTime.UtcNow;
    public DateTime ArrivalDate { get; set; }  
    public string DepartureAirport { get; set; } = string.Empty;
    public string ArrivalAirport { get; set; } = string.Empty;
    public String Airport { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public ICollection<Class> Classes { get; set; } = [];
    public ICollection<FlightReservation> Reservations { get; set; } = [];
}
