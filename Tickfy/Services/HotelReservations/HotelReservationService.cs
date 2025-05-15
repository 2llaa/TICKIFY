using System.Security.Claims;
using Tickfy.Contracts.HotelReservations;
using Tickfy.Entities;
using Tickfy.Persistence;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tickfy.Services.HotelReservations;

public class HotelReservationService(TickfyDBContext tickfyDBContext, IHttpContextAccessor httpContextAccessor) : IHotelReservationService
{
    private readonly TickfyDBContext _context = tickfyDBContext;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<HotelReservationResponse>> GetByIdAsync(int Id, CancellationToken cancellationToken)
    {
        var ourReservation = await _context.HotelReservations.Where(h => h.Id == Id).FirstOrDefaultAsync();

        return (ourReservation is null) ?
            Result.Failure<HotelReservationResponse>(ReservationErrors.NoAnyReservation) :
            Result.Success(ourReservation.Adapt<HotelReservationResponse>());
    }

    public async Task<Result<HotelReservationResponse>> ReserveAsync(HotelReservationRequest request, CancellationToken cancellationToken)
    {
        var roomsById = request.RoomsId;

        List<int> RoomsNotValid = [];

        foreach (var id in roomsById)
        {
            var hotelReservationsId = await _context.HotelReservationRoomss
                .Where(r => r.RoomId == id)
                .Select(r => r.HotelReservationId)
                .ToListAsync();

            var reservations = await _context.HotelReservations
                .Where(hr => hotelReservationsId.Contains(hr.Id))
                .ToListAsync();

            foreach (var reservation in reservations)
            {

                if (Help.IsIntersect(request.CheckInDate, request.CheckOutDate, reservation.CheckInDate, reservation.CheckOutDate))
                {
                    RoomsNotValid.Add(id);
                    break;
                }
            }
        }
        if (RoomsNotValid.Any())
        {
            string Rooms = "Room" + ((RoomsNotValid.Count > 1) ? "s " : " ")
                + ($"With Id [ ");

            foreach (var id in RoomsNotValid)
            {
                Rooms += (id + " ");
            }
            Rooms += ($"] ");
            return Result.Failure<HotelReservationResponse>(ReservationErrors.NotValidRooms(Rooms));
        }

        HotelReservation ourReservation = new HotelReservation();

        var IdFirstItem = roomsById.First();
        var ourRoom = await _context.Rooms.Where(r => r.Id == IdFirstItem).FirstOrDefaultAsync();

        ourReservation.HotelId = ourRoom!.HotelId; //I am sure that all rooms have same HotelId


        var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var ourCustomer = await _context.Users.Where(u => u.Id == currentUserId).FirstOrDefaultAsync();

        ourReservation.CheckInDate = request.CheckInDate;
        ourReservation.CheckOutDate = request.CheckOutDate;
        ourReservation.CreatedBy = ourCustomer!;
        ourReservation.CreatedById = currentUserId!;
        ourReservation.CreatedOn = DateTime.UtcNow;


        decimal totalPrice = 0;
        int daysDiff = (request.CheckOutDate - request.CheckInDate).Days;

        await _context.AddAsync(ourReservation);
        await _context.SaveChangesAsync(cancellationToken);

        foreach (var id in roomsById)
        {
            ourRoom = await _context.Rooms.Where(r => r.Id == id).FirstOrDefaultAsync();
            HotelReservationRoom hotelReservationRoom = new HotelReservationRoom();


            totalPrice += daysDiff * ourRoom!.PricePerNight;

            hotelReservationRoom.RoomId = id;
            hotelReservationRoom.HotelReservationId = ourReservation.Id;


            await _context.AddAsync(hotelReservationRoom);
            await _context.SaveChangesAsync(cancellationToken);

        }

        ourReservation.TotaPrice = totalPrice;

        await _context.SaveChangesAsync(cancellationToken);



        return Result.Success(ourReservation.Adapt<HotelReservationResponse>());

    }
}