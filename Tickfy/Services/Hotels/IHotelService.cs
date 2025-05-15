using Tickfy.Contracts.Hotels;

namespace Tickfy.Services.Hotels;

public interface IHotelService
{
   // Task<Result<IEnumerable<HotelResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<HotelResponse>>> GetFilteredHotelsAsync(List<int>? ratings, CancellationToken cancellationToken);

    //Task<Result<IEnumerable<SearchAvailableHotelsResponse>>> SearchAsync(SearchAvailableHotelsRequest request, CancellationToken cancellationToken);
   // Task<Result<IEnumerable<HotelResponse>>> GetHotelsByStarRatingAsync(int starRating, CancellationToken cancellationToken);

}
