using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using TICKIFY.API.Abstracts;
using TICKIFY.API.Contracts.Drivers;
using TICKIFY.API.Contracts.hotels;
using TICKIFY.API.Contracts.Requests;
using TICKIFY.API.Contracts.Rooms;
using TICKIFY.API.Errors.Hotel;
using TICKIFY.API.Persistence.Data;
using TICKIFY.API.Services.Abstracts;
using TICKIFY.Data.Entities;
using TICKIFY.Data.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

public class HotelServices : IHotelServices
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public HotelServices(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<HotelRes>>> GetAllHotelsAsync(CancellationToken cancellationToken)
    {
        var hotels = await _context.Hotels
            .Include(h => h.Drivers)
            .ToListAsync(cancellationToken);

        return Result.Success(hotels.Adapt<IEnumerable<HotelRes>>());
    }

    public async Task<Result<HotelRes>> GetHotelByIdAsync(int id, CancellationToken cancellationToken)
    {
        var hotel = await _context.Hotels
            .Include(h => h.Drivers)
            .FirstOrDefaultAsync(h => h.HotelId == id, cancellationToken);

        return hotel == null
            ? Result.Failure<HotelRes>(HotelErrors.HotelNotFound)
            : Result.Success(hotel.Adapt<HotelRes>());
    }

    public async Task<Result<IEnumerable<SearchHotelRes>>> GetHotelsByNameAsync(string name, CancellationToken cancellationToken)
    {
        var query = _context.Hotels.AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            if (Enum.TryParse<HotelName>(name, true, out var hotelNameEnum))
            {
                query = query.Where(h => h.Name == hotelNameEnum);
            }
            else
            {
                return Result.Failure<IEnumerable<SearchHotelRes>>(HotelErrors.HotelNotFound);
            }
        }
        var hotels = await query
            .Include(h => h.Drivers)  
            .Include(h => h.Rooms)
            .ToListAsync();

        if (!hotels.Any())
        {
            return Result.Failure<IEnumerable<SearchHotelRes>>(HotelErrors.HotelNotFound);
        }
       return Result.Success(hotels.Adapt<IEnumerable<SearchHotelRes>>());

    }
    public async Task<Result<IEnumerable<RoomRes>>> GetHotelRoomsAsync(int hotelId, CancellationToken cancellationToken)
    {
        var hotelExists = await _context.Hotels.AnyAsync(h => h.HotelId == hotelId, cancellationToken);
        if (!hotelExists)
            return Result.Failure<IEnumerable<RoomRes>>(HotelErrors.HotelNotFound);

        var rooms = await _context.Rooms
            .Where(r => r.HotelId == hotelId)
            .ToListAsync(cancellationToken);

        return rooms.Any()
            ? Result.Success(rooms.Adapt<IEnumerable<RoomRes>>())
            : Result.Failure<IEnumerable<RoomRes>>(HotelErrors.NoRoomsForHotel);
    }

    public async Task<Result<IEnumerable<HotelRes>>> GetHotelsByLocationAsync(string location, CancellationToken cancellationToken)
    {
        var hotels = await _context.Hotels
            .Where(h => h.Location.ToLower().Contains(location.ToLower()))
            .ToListAsync(cancellationToken);

        return hotels.Any()
            ? Result.Success(hotels.Adapt<IEnumerable<HotelRes>>())
            : Result.Failure<IEnumerable<HotelRes>>(HotelErrors.HotelNotFound);
    }

    public async Task<Result<IEnumerable<HotelRes>>> GetHotelsByStarRatingAsync(int stars, CancellationToken cancellationToken)
    {
        var hotels = await _context.Hotels
            .Where(h => h.StarRating == stars)
            .ToListAsync(cancellationToken);

        return hotels.Any()
            ? Result.Success(hotels.Adapt<IEnumerable<HotelRes>>())
            : Result.Failure<IEnumerable<HotelRes>>(HotelErrors.HotelNotFound);
    }

    public async Task<Result<HotelRes>> CreateHotelAsync(HotelReq hotelReq, CancellationToken cancellationToken)
    {
        var exists = await _context.Hotels.AnyAsync(h => h.Name.ToString() == hotelReq.Name, cancellationToken);
        if (exists)
            return Result.Failure<HotelRes>(HotelErrors.DuplicateHotel);

        var hotel = _mapper.Map<Hotels>(hotelReq);
        _context.Hotels.Add(hotel);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(hotel.Adapt<HotelRes>());
    }

    public async Task<Result<HotelRes>> UpdateHotelAsync(int id, HotelReq hotelReq, CancellationToken cancellationToken)
    {
        var existing = await _context.Hotels.FirstOrDefaultAsync(h => h.HotelId == id, cancellationToken);
        if (existing == null)
            return Result.Failure<HotelRes>(HotelErrors.HotelNotFound);

        existing = _mapper.Map(hotelReq, existing);
        _context.Hotels.Update(existing);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(existing.Adapt<HotelRes>());
    }

    public async Task<Result<bool>> DeleteHotelAsync(int id, CancellationToken cancellationToken)
    {
        var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.HotelId == id, cancellationToken);
        if (hotel == null)
            return Result.Failure<bool>(HotelErrors.HotelNotFound);

        _context.Hotels.Remove(hotel);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(true);
    }

    public async Task<Result<IEnumerable<DriverRes>>> GetHotelDriversAsync(int hotelId, CancellationToken cancellationToken)
    {
        var exists = await _context.Hotels.AnyAsync(h => h.HotelId == hotelId, cancellationToken);
        if (!exists)
            return Result.Failure<IEnumerable<DriverRes>>(HotelErrors.HotelNotFound);

        var drivers = await _context.Drivers
            .Where(d => d.HotelId == hotelId)
            .ToListAsync(cancellationToken);

        return drivers.Any()
            ? Result.Success(drivers.Adapt<IEnumerable<DriverRes>>())
            : Result.Failure<IEnumerable<DriverRes>>(HotelErrors.NoDriversForHotel);
    }
}
