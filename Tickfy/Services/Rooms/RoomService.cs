using Tickfy.Contracts.Rooms;
using Tickfy.Enums;
using Tickfy.Persistence;

namespace Tickfy.Services.Rooms;

public class RoomService(TickfyDBContext context) : IRoomService
{
    private readonly TickfyDBContext _context = context;

    public async Task<Result<IEnumerable<RoomReservationsDatesResponse>>> GetRoomReservationsDatesAsync(int HotelId,int Id, CancellationToken cancellationToken)
    {
        var reservations = await _context.HotelReservationRoomss
            .Where(hr => hr.RoomId == Id).ToListAsync();

        return reservations.Any() ?
             Result.Success<IEnumerable<RoomReservationsDatesResponse>>(reservations.Adapt<IEnumerable<RoomReservationsDatesResponse>>()):
             Result.Failure<IEnumerable<RoomReservationsDatesResponse>>(RoomError.EmptyReservationsResults);
    }

    public async Task<Result<IEnumerable<RoomResponse>>> SelectRoomAsync(SelectRoomRequest request,int HotelId, CancellationToken cancellation)
    {

        var allRooms = await _context.Rooms
            .Include(r => r.Beds)
            .Where(r => r.HotelId == HotelId)
            .Where(r => r.RoomType == (RoomType)request.RoomType)
            .Where(r => request.BedType == null || r.Beds.Any(b => b.Type == (BedType)request.BedType))
            .Where(r => request.OpptionalBedType == null || r.Beds.Any(b => b.Type == (BedType)request.OpptionalBedType))
            .ToListAsync();
        List<Room> ourRooms = [];

        foreach (var room in allRooms)
        {
            var hotelReservationsId =await _context.HotelReservationRoomss 
                .Where(r => r.RoomId == room.Id)
                .Select(r => r.HotelReservationId)
                .ToListAsync();

            var reservations = await _context.HotelReservations
                .Where(hr => hotelReservationsId.Contains(hr.Id))
                .ToListAsync();

            bool isValid = true;
            foreach(var reservation in reservations)
            {

                if (Help.IsIntersect(request.CheckInDate, request.CheckOutDate, reservation.CheckInDate, reservation.CheckOutDate))
                {
                    isValid = false; 
                    break;
                }
            }
            if (isValid)
                ourRooms.Add(room);
        }


        return ourRooms.Any() ?
            Result.Success<IEnumerable<RoomResponse>>(ourRooms.Adapt<IEnumerable<RoomResponse>>()) :
            Result.Failure<IEnumerable<RoomResponse>>(RoomError.RoomNotFound);


    }


}
