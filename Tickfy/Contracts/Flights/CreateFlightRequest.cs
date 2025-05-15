using Tickfy.Contracts.Classes;

namespace Tickfy.Contracts.Flights;

public class CreateFlightRequest
{
    public DateTime DepartureDate { get; set; }
    public DateTime ArrivalDate { get; set; }
    public string DepartureAirport { get; set; } = string.Empty;
    public string ArrivalAirport { get; set; } = string.Empty;
    public String Airport {  get; set; } = string.Empty;
    public ICollection<CreateClassRequest> Classes { get; set; } = [];
}