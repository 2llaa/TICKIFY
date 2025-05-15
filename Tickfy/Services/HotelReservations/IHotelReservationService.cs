using Tickfy.Contracts.HotelReservations;

namespace Tickfy.Services.HotelReservations;

public interface IHotelReservationService
{
    Task<Result<HotelReservationResponse>> GetByIdAsync(int Id, CancellationToken cancellationToken);
    Task<Result<HotelReservationResponse>> ReserveAsync(HotelReservationRequest request, CancellationToken cancellationToken);
}
