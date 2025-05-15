using System.Linq;
using Tickfy.Contracts.Drivers;
using Tickfy.Persistence;

namespace Tickfy.Services.Driver
{
    public class DriverService(TickfyDBContext tickfyDBContext, IHttpContextAccessor httpContextAccessor) : IDriverService
    {

        private readonly TickfyDBContext _context = tickfyDBContext;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        public async Task<Result<IEnumerable<DriverResponse>>> GetDriversByStarRatingDescendingAsync(CancellationToken cancellationToken = default)
        {
            var drivers = await _context.Drivers
                .AsNoTracking()
                .OrderByDescending(d => d.StarRating)
                .Select(d => new DriverResponse(
                    d.DriverId,
                    d.DriverName,
                    d.CarType,
                    d.Price,
                    d.StarRating
                ))
                .ToListAsync(cancellationToken);

            return drivers.Any()
                ? Result.Success<IEnumerable<DriverResponse>>(drivers)
                : Result.Failure<IEnumerable<DriverResponse>>(DriverErrors.NoAnyDriver);
        }

        public async Task<Result<IEnumerable<DriverResponse>>> GetDriversByPriceAscendingAsync(CancellationToken cancellationToken = default)
        {
            var drivers = await _context.Drivers
                .AsNoTracking()
                .OrderBy(d => d.Price)
                .Select(d => new DriverResponse(
                    d.DriverId,
                    d.DriverName,
                    d.CarType,
                    d.Price,
                    d.StarRating
                ))
                .ToListAsync(cancellationToken);

            return drivers.Any()
                ? Result.Success<IEnumerable<DriverResponse>>(drivers)
                : Result.Failure<IEnumerable<DriverResponse>>(DriverErrors.NoAnyDriver);
        }
    }

}
