using Tickfy.Contracts.Drivers;

namespace Tickfy.Services.Driver
{
    public interface IDriverService
    {
        Task<Result<IEnumerable<DriverResponse>>> GetDriversByStarRatingDescendingAsync(CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<DriverResponse>>> GetDriversByPriceAscendingAsync(CancellationToken cancellationToken = default);
    }
}
