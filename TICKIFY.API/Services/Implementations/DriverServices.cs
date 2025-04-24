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
using TICKIFY.Data.Enums;
using TICKIFY.API.Errors.Reservation;

namespace TICKIFY.API.Services.Implementations;

public class DriverServices(ApplicationDbContext context, IMapper mapper) : IDriverServices
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<DriverRes>> GetDriverByIdAsync(int id, CancellationToken cancellationToken)
    {
        var driver = await _context.Drivers
            .Include(d => d.Hotel)
            .FirstOrDefaultAsync(d => d.DriverId == id && !d.IsDeleted, cancellationToken);

        if (driver is null)
            return Result.Failure<DriverRes>(DriverErrors.DriverNotFound); 

        return Result.Success(driver.Adapt<DriverRes>());
    }



    public async Task<Result<DriverRes>> CreateDriverAsync(DriverReq driverReq, CancellationToken cancellationToken)
    {
        var hotelExists = await _context.Hotels
            .AnyAsync(h => h.HotelId == driverReq.HotelId, cancellationToken);

        if (!hotelExists)
            return Result.Failure<DriverRes>(HotelErrors.HotelNotFound);
        if (!Enum.TryParse<DriverCarType>(driverReq.CarType, true, out var carType))
        {
            return Result.Failure<DriverRes>(DriverErrors.DriverNotFound);
        }

        var driver = driverReq.Adapt<Drivers>();
        driver.CarType = carType;  

        await _context.Drivers.AddAsync(driver, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(driver.Adapt<DriverRes>());
    }



    public async Task<Result<DriverRes>> UpdateDriverAsync(int id, DriverReq driverReq, CancellationToken cancellationToken)
    {
        var driver = await _context.Drivers
            .FirstOrDefaultAsync(d => d.DriverId == id && !d.IsDeleted, cancellationToken);

        if (driver is null)
            return Result.Failure<DriverRes>(DriverErrors.DriverNotFound);

        _mapper.Map(driverReq, driver); 

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

    public async Task<Result> SoftDeleteDriverAsync(int id, CancellationToken cancellationToken)
    {
        var driver = await _context.Drivers
     .FirstOrDefaultAsync(r => r.DriverId == id, cancellationToken);

        if (driver == null)
            return Result.Failure(DriverErrors.DriverNotFound);
        driver.IsDeleted = true;
        driver.DeletedAt = DateTime.UtcNow;
        driver.Status = "Cancelled";

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}



