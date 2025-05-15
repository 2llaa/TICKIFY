using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq;
using Tickfy.Contracts.Classes;
using Tickfy.Contracts.Flights;
using Tickfy.Entities;
using Tickfy.Persistence;

namespace Tickfy.Services.Flights;

public class FlightService(TickfyDBContext context) : IFlightService
{
    private readonly TickfyDBContext _context = context;



    public async Task<Result<IEnumerable<FlightResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var flights = await _context.Flights
            .Where(f => f.IsActive)
            .Include(f => f.Classes)
            .Select(f => new FlightResponse(
                f.Id,
                f.DepartureDate,
                f.ArrivalDate,
                f.DepartureAirport,
                f.ArrivalAirport,
                f.Airport,
                f.IsActive,
                f.Classes.Select(c => new ClassResponse(c.Id, c.className, c.Price, c.AvailableSeats,c.Capacity))
            ))
            .AsNoTracking()
            .ToListAsync(cancellationToken);


        return (flights.Any()) ?
        Result.Success<IEnumerable<FlightResponse>>(flights.Adapt<IEnumerable<FlightResponse>>()):
        Result.Failure<IEnumerable<FlightResponse>>(FlightErrors.NoAnyFlight);
    }

    public async Task<Result<FlightByIdResponse>> GetAsync(int id, CancellationToken cancellationToken = default) {

        var flight = await _context.Flights
         .Include(f => f.Classes) 
         .FirstOrDefaultAsync(f => f.Id == id);

        if (flight is null || !flight.IsActive)
            return Result.Failure<FlightByIdResponse>(FlightErrors.FlightNotFound);

        return Result.Success(flight.Adapt<FlightByIdResponse>());

    }
    public async Task<Result<IEnumerable<SearchFlightResponse>>> SearchFlightAsync(SearchFlightRequest flight, CancellationToken cancellationToken = default)
    {

        var validFlights = await _context.Flights.AsNoTracking()
            .Where(f => f.DepartureAirport == flight.DepartureAirport
                      && f.ArrivalAirport == flight.ArrivalAirport
                      && f.DepartureDate >= flight.DepartureDate
                      && f.IsActive)
            .OrderBy(f => f.DepartureDate)
            .ThenBy(f => f.ArrivalDate)
            .ThenBy(f => f.DepartureAirport)
            .Include(f => f.Classes)
            .Select(f => new SearchFlightResponse(
                f.Id,
                f.DepartureDate,
                f.ArrivalDate,
                f.DepartureAirport,
                f.ArrivalAirport,
                f.Airport,
                f.Classes.Select(c => new ClassResponse(c.Id, c.className, c.Price, c.AvailableSeats,c.Capacity))
             ))
            .AsNoTracking()
            .ToListAsync(cancellationToken);


        return (validFlights.Any()) ?
             Result.Success<IEnumerable<SearchFlightResponse>>(validFlights)
             : Result.Failure<IEnumerable<SearchFlightResponse>>(FlightErrors.EmptyFlightResults);

    }

    public async Task<Result<CreateFlightResponse>> AddAsync(CreateFlightRequest flightRequest, CancellationToken cancellationToken = default)
    {

        var HasSameFlight = await _context.Flights
                .AnyAsync(x =>
                     x.IsActive&&
                     x.Airport == flightRequest.Airport &&
                     x.DepartureAirport == flightRequest.DepartureAirport &&
                     x.ArrivalAirport == flightRequest.ArrivalAirport &&
                     x.DepartureDate == flightRequest.DepartureDate
                     , cancellationToken);

        if (HasSameFlight)
            return Result.Failure<CreateFlightResponse>(FlightErrors.DuplicateFlight);

        var flight = flightRequest.Adapt<Flight>();

        await _context.Flights.AddAsync(flight,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

     var ourNewFlight = await _context.Flights
            .OrderByDescending(f => f.Id) 
            .FirstOrDefaultAsync(cancellationToken);

        return
            Result.Success(ourNewFlight.Adapt<CreateFlightResponse>());
    }

    public async Task<Result<FlightResponse>> UpdateDateAsync(int id, UpdateDateFlightRequest flight, CancellationToken cancellationToken = default)
    {
        var existedFlight = await _context.Flights.FindAsync(id, cancellationToken);

        if (existedFlight is null || !existedFlight.IsActive)
            return Result.Failure<FlightResponse>(FlightErrors.EmptyFlightResults);


        var HasSameFlight =  await _context.Flights
                 .AnyAsync(x => x.Id != existedFlight.Id &&
                            x.IsActive &&
                            x.Airport == existedFlight.Airport &&
                            x.DepartureAirport == existedFlight.DepartureAirport &&
                            x.ArrivalAirport == existedFlight.ArrivalAirport &&
                            x.DepartureDate == existedFlight.DepartureDate
                            , cancellationToken);

        if (HasSameFlight)
            return Result.Failure<FlightResponse>(FlightErrors.DuplicateFlight);

        existedFlight.ArrivalDate = flight.ArrivalDate;
        existedFlight.DepartureDate = flight.DepartureDate; 

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(existedFlight.Adapt<FlightResponse>());
    }
}
  
