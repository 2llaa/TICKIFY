using Microsoft.EntityFrameworkCore;
using TICKIFY.API.Abstracts;
using TICKIFY.API.Contracts.Drivers;
using TICKIFY.API.Contracts.Requests;
using TICKIFY.API.Errors;
using TICKIFY.API.Errors.Hotel;
using TICKIFY.API.Persistence.Data;
using TICKIFY.API.Services.Abstracts;
using TICKIFY.Data.Entities;
using Mapster;
using MapsterMapper;

namespace TICKIFY.API.Services.Implementations;

public class DriverServices(ApplicationDbContext context, IMapper mapper) : IDriverServices
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<IEnumerable<DriverRes>>> GetAllDriversAsync(CancellationToken cancellationToken)
    {
        var drivers = await _context.Drivers
            .Include(d => d.Hotel)
            .ToListAsync(cancellationToken);

        var response = drivers.Adapt<IEnumerable<DriverRes>>();
        return Result.Success(response);
    }

    public async Task<Result<DriverRes>> GetDriverByIdAsync(int id, CancellationToken cancellationToken)
    {
        var driver = await _context.Drivers
            .Include(d => d.Hotel)
            .FirstOrDefaultAsync(d => d.DriverId == id, cancellationToken);

        if (driver is null)
            return Result.Failure<DriverRes>(DriverErrors.DriverNotFound);

        return Result.Success(driver.Adapt<DriverRes>());
    }

    public async Task<Result<IEnumerable<DriverRes>>> GetDriversByHotelIdAsync(int hotelId)
    {
        var hotelExists = await _context.Hotels
            .AnyAsync(h => h.HotelId == hotelId);

        if (!hotelExists)
            return Result.Failure<IEnumerable<DriverRes>>(HotelErrors.HotelNotFound);

        var drivers = await _context.Drivers
            .Where(d => d.HotelId == hotelId)
            .Include(d => d.Hotel)
            .ToListAsync();

        if (!drivers.Any())
            return Result.Failure<IEnumerable<DriverRes>>(DriverErrors.NoDriversForHotel);

        return Result.Success(drivers.Adapt<IEnumerable<DriverRes>>());
    }

    public async Task<Result<DriverRes>> CreateDriverAsync(DriverReq driverReq, CancellationToken cancellationToken)
    {
        var hotelExists = await _context.Hotels
            .AnyAsync(h => h.HotelId == driverReq.HotelId, cancellationToken);

        if (!hotelExists)
            return Result.Failure<DriverRes>(HotelErrors.HotelNotFound);

        var driver = driverReq.Adapt<Drivers>();

        await _context.Drivers.AddAsync(driver, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(driver.Adapt<DriverRes>());
    }

    public async Task<Result<DriverRes>> UpdateDriverAsync(int id, DriverReq driverReq, CancellationToken cancellationToken)
    {
        var driver = await _context.Drivers
            .FirstOrDefaultAsync(d => d.DriverId == id, cancellationToken);

        if (driver is null)
            return Result.Failure<DriverRes>(DriverErrors.DriverNotFound);

        _mapper.Map(driverReq, driver); // Updates the existing driver

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(driver.Adapt<DriverRes>());
    }

    public async Task DeleteDriverAsync(int id, CancellationToken cancellationToken)
    {
        var driver = await _context.Drivers
            .FirstOrDefaultAsync(d => d.DriverId == id, cancellationToken);

        if (driver is not null)
        {
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}



