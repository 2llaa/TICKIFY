using Tickfy.Entities;

namespace Tickfy.Services.FlightReservations;

public interface IFlightReservationService
{
    Task<FlightReservation?> GetAsync(int id, CancellationToken cancellationToken = default!);
    Task<FlightReservation?>AddAsync(int flightId, int classId, CancellationToken cancellationToken = default!);

}
