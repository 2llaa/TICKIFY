using Tickfy.Contracts.Classes;
using Tickfy.Contracts.Flights;
using Tickfy.Entities;

namespace Tickfy.Services.Flights;

public interface IFlightService
{

    Task<Result<IEnumerable<FlightResponse>>> GetAllAsync(CancellationToken cancellationToken = default!);
    Task<Result<IEnumerable<SearchFlightResponse>>> SearchFlightAsync(SearchFlightRequest flight, CancellationToken cancellationToken = default!);
    Task<Result<FlightByIdResponse>>GetAsync(int id, CancellationToken cancellationToken = default!);
    Task<Result<CreateFlightResponse>> AddAsync(CreateFlightRequest flight, CancellationToken cancellationToken = default!);
    Task<Result<FlightResponse>> UpdateDateAsync(int id, UpdateDateFlightRequest flight, CancellationToken cancellationToken = default!);


}
