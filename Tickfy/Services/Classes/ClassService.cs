using Microsoft.EntityFrameworkCore;
using Tickfy.Contracts.Classes;
using Tickfy.Entities;
using Tickfy.Persistence;

namespace Tickfy.Services.Classes;

public class ClassService(TickfyDBContext context) : IClassService
{
    private readonly TickfyDBContext _context = context;


    public async Task<IEnumerable<Class>> GetAsync(int flightId, CancellationToken cancellationToken)=>
         await _context.Classes
                 .Where(f => f.FlightId == flightId)
                 .ToListAsync(cancellationToken);

    public async Task<Result<CreateClassResponse>> AddAsync(int flightId,CreateClassRequest classRequest, CancellationToken cancellationToken = default)
    {
        var flight = await _context.Flights.FindAsync(flightId);
        if(flight is null || !flight.IsActive)
            return Result.Failure<CreateClassResponse>(FlightErrors.FlightNotFound);

        var HasSameClass = await _context.Classes
                            .AnyAsync(c => c.FlightId == flightId && c.className == classRequest.className);

        if (HasSameClass)
            return Result.Failure<CreateClassResponse>(ClassErrors.DuplicateClass);

        Class ourNewClass = new Class
        {
            className = classRequest.className,
            Price = classRequest.Price,
            Capacity = classRequest.Capacity,
            AvailableSeats = classRequest.AvailableSeats,
            Flight = flight,
            FlightId = flightId
        };

        await _context.Classes.AddAsync(ourNewClass);
        await _context.SaveChangesAsync(cancellationToken);

        var myClass = await _context.Classes
           .OrderByDescending(c => c.Id)
           .FirstOrDefaultAsync(cancellationToken);

        var classResponse = myClass.Adapt<CreateClassResponse>();

        return Result.Success(classResponse);
    }

    public async Task<Result<ClassResponse>> GetAsync(int flightID, int classID, CancellationToken cancellationToken)
    {
        var ThereIsAFlight = await _context.Flights
            .AnyAsync(f => f.Id == flightID && f.IsActive);

        if (!ThereIsAFlight)
            return Result.Failure<ClassResponse>(FlightErrors.FlightNotFound);

        var ourClass = await _context.Classes
            .Where(c => c.FlightId == flightID && c.Id == classID)
            .FirstOrDefaultAsync(cancellationToken);

        if (ourClass is null)
            return Result.Failure<ClassResponse>(ClassErrors.ClassNotFound);

        return Result.Success(ourClass.Adapt<ClassResponse>());
    }
}
