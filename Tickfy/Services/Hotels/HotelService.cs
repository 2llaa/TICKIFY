
using System.Linq;
using Tickfy.Contracts.BedInfos;
using Tickfy.Contracts.Hotels;
using Tickfy.Contracts.Rooms;
using Tickfy.Enums;
using Tickfy.Persistence;

namespace Tickfy.Services.Hotels;

public class HotelService(TickfyDBContext context) : IHotelService
{
    private readonly TickfyDBContext _context = context;

    //public async Task<Result<IEnumerable<HotelResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    //{

    //    var Hotels = await _context.Hotels
    //        .Include(h => h.Rooms)
    //        .ThenInclude(r => r.Beds)
    //        .Select(h => new HotelResponse(
    //            h.Id,
    //            (int)h.StarRating,
    //            h.Name,
    //            h.Rooms
    //                .Select(r => new RoomResponse(
    //                    r.Id,
    //                    r.RoomType,
    //                    //r.RoomNumber,
    //                    r.PricePerNight,
    //                    r.Beds
    //                        .Select(b => new BedInfoResponse(b.Type, b.Count))
    //                        .ToList()
    //                ))
    //                .ToList()
    //        ))
    //        .AsNoTracking()
    //        .ToListAsync(cancellationToken);

    //    return Hotels.Any() ?
    //        Result.Success<IEnumerable<HotelResponse>>(Hotels.Adapt<IEnumerable<HotelResponse>>()) :
    //        Result.Failure<IEnumerable<HotelResponse>>(HotelErrors.NoAnyHotel);
    //}

    //public async Task<Result<IEnumerable<HotelResponse>>> GetHotelsByStarRatingAsync(int starRating, CancellationToken cancellationToken)
    //{
    //    if (!Enum.IsDefined(typeof(StarRating), starRating))
    //    {
    //        return Result.Failure<IEnumerable<HotelResponse>>(HotelErrors.InvalidStarRating);
    //    }

    //    var ratingEnum = (StarRating)starRating;

    //    var hotels = await _context.Hotels
    //        .Include(h => h.Rooms)
    //        .ThenInclude(r => r.Beds)
    //        .Where(h => h.StarRating == ratingEnum)
    //        .AsNoTracking()
    //        .ToListAsync(cancellationToken);

    //    return hotels.Any()
    //        ? Result.Success(hotels.Adapt<IEnumerable<HotelResponse>>())
    //        : Result.Failure<IEnumerable<HotelResponse>>(HotelErrors.NoAnyHotel);
    //}
    public async Task<Result<IEnumerable<HotelResponse>>> GetFilteredHotelsAsync(
    List<int>? ratings,
    CancellationToken cancellationToken = default)
    {
        var query = _context.Hotels
            .Include(h => h.Rooms)
                .ThenInclude(r => r.Beds)
            .AsNoTracking()
            .AsQueryable();

        // if user want spicified rating
        if (ratings is not null && ratings.Any())
        {
            var validRatings = ratings
                .Where(r => Enum.IsDefined(typeof(StarRating), r))
                .Select(r => (StarRating)r)
                .ToList();

            if (!validRatings.Any())
                return Result.Failure<IEnumerable<HotelResponse>>(HotelErrors.InvalidStarRating);

            query = query.Where(h => validRatings.Contains(h.StarRating));
        }

        // descending order
        var hotels = await query
            .OrderByDescending(h => h.StarRating)
            .ToListAsync(cancellationToken);

        var hotelResponses = hotels.Select(h => new HotelResponse(
            h.Id,
            (int)h.StarRating,
            h.Name,
            h.Rooms.Select(r => new RoomResponse(
                r.Id,
                r.RoomType,
                r.PricePerNight,
                r.Beds.Select(b => new BedInfoResponse(b.Type, b.Count)).ToList()
            )).ToList()
        )).ToList();

        return hotelResponses.Any()
            ? Result.Success<IEnumerable<HotelResponse>>(hotelResponses)
            : Result.Failure<IEnumerable<HotelResponse>>(HotelErrors.NoAnyHotel);
    }




}
