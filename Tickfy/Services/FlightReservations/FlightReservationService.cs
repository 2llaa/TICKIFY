
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Tickfy.Entities;
using Tickfy.Persistence;

namespace Tickfy.Services.FlightReservations;

public class FlightReservationService(TickfyDBContext context,IHttpContextAccessor httpContextAccessor) : IFlightReservationService
{
    private readonly TickfyDBContext _context = context;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<FlightReservation?> GetAsync(int id, CancellationToken cancellationToken = default) =>
           await _context.FlightReservationS.FindAsync(id);
    
    public async Task<FlightReservation?> AddAsync(int flightId, int classId, CancellationToken cancellationToken = default)
    {
        var ourFlight = await _context.Flights.Where(f => f.Id == flightId).FirstOrDefaultAsync();
        //check that my Class in the same Flight

        var ourClass = await _context.Classes.Where(c => c.Id == classId).FirstOrDefaultAsync();

        if (ourClass is null || ourFlight is null|| ourClass.AvailableSeats == 0)
            return null;

        var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var ourCustomer = await _context.Users.Where(u => u.Id == currentUserId).FirstOrDefaultAsync();

        int flightSeatNumber = ourClass.Capacity - ourClass.AvailableSeats + 1;
        ourClass.AvailableSeats--;


        FlightReservation ourFlightReservation = new FlightReservation
        {
            SeatNumber = flightSeatNumber,
            Flight = ourFlight,
            FlightId = flightId,
            Class = ourClass,
            ClassId = classId,
            CreatedById = currentUserId!,
            CreatedBy = ourCustomer!,
            CreatedOn = DateTime.UtcNow,
        };
        ourClass.Reservations.Add(ourFlightReservation);
        ourFlight.Reservations.Add(ourFlightReservation);



        await _context.FlightReservationS.AddAsync(ourFlightReservation);
        await _context.SaveChangesAsync(cancellationToken);


        return ourFlightReservation;


    }

   
}


