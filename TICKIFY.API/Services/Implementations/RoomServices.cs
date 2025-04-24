using TICKIFY.API.Services.Abstracts;
using TICKIFY.Data.Entities;
using TICKIFY.Data.Enums;
using Mapster;
using Microsoft.EntityFrameworkCore;
using TICKIFY.API.Persistence.Data;
using TICKIFY.API.Errors;
using TICKIFY.API.Contracts.Rooms;
using TICKIFY.API.Abstracts;
using TICKIFY.API.Errors.Room;
using MapsterMapper;
using TICKIFY.API.Errors.Hotel;

namespace TICKIFY.API.Services.Implementations
{
    public class RoomServices : IRoomServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RoomServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<Result<IEnumerable<RoomRes>>> GetAllRoomsAsync(CancellationToken cancellationToken)
        {
            var rooms = await _context.Rooms
                .Where(r => !r.IsDeleted)
                .ToListAsync(cancellationToken);

            if (!rooms.Any())
            {
                return Result.Failure<IEnumerable<RoomRes>>(RoomErrors.NoRoomsFound);
            }
            return Result.Success(rooms.Adapt<IEnumerable<RoomRes>>());
        }


        public async Task<Result<RoomRes>> GetRoomByIdAsync(int id, CancellationToken cancellationToken)
        {
            var room = await _context.Rooms
                .FirstOrDefaultAsync(r => r.RoomId == id && !r.IsDeleted, cancellationToken); 
            if (room == null)
                return Result.Failure<RoomRes>(RoomErrors.RoomNotFound);

            return Result.Success(room.Adapt<RoomRes>());
        }


        public async Task<Result<decimal>> GetRoomPriceByIdAsync(int id, CancellationToken cancellationToken)
        {
            var room = await _context.Rooms
                .FirstOrDefaultAsync(r => r.RoomId == id && !r.IsDeleted, cancellationToken); 
            return room == null
                ? Result.Failure<decimal>(RoomErrors.RoomNotFound)
                : Result.Success(room.PricePerNight);
        }

        // Get a specific room by hotel and room ID
        public async Task<Result<RoomRes>> GetRoomByHotelAsync(int hotelId, int roomId, CancellationToken cancellationToken)
        {
            var room = await _context.Rooms
                .FirstOrDefaultAsync(r => r.HotelId == hotelId && r.RoomId == roomId && !r.IsDeleted, cancellationToken); 
            return room == null
                ? Result.Failure<RoomRes>(RoomErrors.RoomNotFound)
                : Result.Success(room.Adapt<RoomRes>());
        }


        // Get hotel room status for a specific date and time 
        public async Task<Result<string>> GetRoomStatusAtTimeAsync(int roomId, DateTime dateTime, CancellationToken cancellationToken)
        {
            var room = await _context.Rooms
                .Include(r => r.HotelReservations)
                .FirstOrDefaultAsync(r => r.RoomId == roomId, cancellationToken);

            if (room == null)
            {
                return Result.Failure<string>(RoomErrors.RoomNotFound);
            }
            var reservation = room.HotelReservations.FirstOrDefault(r => r.CheckInDate <= dateTime && r.CheckOutDate >= dateTime);

            if (reservation != null)
            {
                return Result.Success(reservation.Status); 
            }

            return Result.Success("Available"); 
        }
        // Get available rooms for a specific date

        public async Task<Result<IEnumerable<RoomRes>>> GetAvailableRoomsForDateAsync(DateTime dateTime, int hotelId, CancellationToken cancellationToken)
        {
            var targetDate = dateTime;

            var rooms = await _context.Rooms
              .Where(r => r.Status == "Available" && r.HotelId == hotelId && !r.IsDeleted).ToListAsync(cancellationToken);

            Console.WriteLine("Rooms with status 'Available': " + rooms.Count);

            var reservations = await _context.HotelReservations
                .Where(res => res.HotelId == hotelId && res.Status == "Booked")
                .ToListAsync(cancellationToken);

            foreach (var res in reservations)
            {
                Console.WriteLine($"Reservation ID: {res.HotelReservationId}, Room ID: {res.RoomId}, From: {res.CheckInDate}, To: {res.CheckOutDate}");
            }

            var unavailableRoomIds = reservations
                .Where(res =>
                    (targetDate >= res.CheckInDate && targetDate < res.CheckOutDate) ||
                    (targetDate < res.CheckOutDate && targetDate.AddHours(1) > res.CheckInDate)
                )
                .Select(res => res.RoomId)
                .Distinct()
                .ToList();

            Console.WriteLine("Unavailable room IDs: " + string.Join(", ", unavailableRoomIds));

            var availableRooms = rooms
                .Where(r => !unavailableRoomIds.Contains(r.RoomId))
                .ToList();

            Console.WriteLine("Available rooms after filtering: " + availableRooms.Count);

            if (availableRooms.Any())
            {
                var result = availableRooms.Adapt<IEnumerable<RoomRes>>();
                return Result.Success(result);
            }

            return Result.Failure<IEnumerable<RoomRes>>(RoomErrors.RoomNotFound);
        }


        public async Task<Result<IEnumerable<RoomRes>>> GetRoomsByTypeAsync(string type, CancellationToken cancellationToken)
        {
            if (!Enum.TryParse<RoomType>(type, true, out var parsedType))
                return Result.Failure<IEnumerable<RoomRes>>(RoomErrors.RoomTypeNotFound); 

            var rooms = await _context.Rooms
             .Where(r => r.Type == parsedType && !r.IsDeleted).ToListAsync();
            return !rooms.Any()
                ? Result.Failure<IEnumerable<RoomRes>>(RoomErrors.RoomTypeNotFound)
                : Result.Success(rooms.Adapt<IEnumerable<RoomRes>>());
        }

        public async Task<Result<IEnumerable<RoomRes>>> GetRoomsByHotelAsync(int hotelId, CancellationToken cancellationToken)
        {
            var rooms = await _context.Rooms
                .Where(r => r.HotelId == hotelId && !r.IsDeleted)
                .ToListAsync(cancellationToken);

            return !rooms.Any()
                ? Result.Failure<IEnumerable<RoomRes>>(RoomErrors.NoRoomsForHotel)
                : Result.Success(rooms.Adapt<IEnumerable<RoomRes>>());
        }

        public async Task<Result<HotelRoomsRes>> CreateRoomAsync(RoomReq roomReq, CancellationToken cancellationToken)
        {
            var hotelExists = await _context.Hotels
                .AnyAsync(h => h.HotelId == roomReq.HotelId, cancellationToken);

            if (!hotelExists)
                return Result.Failure<HotelRoomsRes>(HotelErrors.NoHotelsFound);

            var roomNumberExists = await _context.Rooms
                .AnyAsync(r => r.HotelId == roomReq.HotelId &&
                              r.RoomNumber == roomReq.RoomNumber,
                       cancellationToken);

            if (roomNumberExists)
                return Result.Failure<HotelRoomsRes>(RoomErrors.DuplicateRoomNumber);

            var room = roomReq.Adapt<Rooms>();
            if (string.IsNullOrEmpty(room.Status))
            {
                room.Status = "Available";
            }

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync(cancellationToken);
            var result = room.Adapt<HotelRoomsRes>();
            return Result.Success(result);
        }


        public async Task<Result<IEnumerable<RoomDto>>> GetAvailableRoomsAsync(RoomAvailabilityRequest request)
        {
            var availableRooms = await _context.Rooms
               .Where(r => r.HotelId == request.HotelId && !r.IsDeleted)
               .Where(r => !_context.HotelReservations.Any(res =>
                   res.RoomId == r.RoomId &&
                   request.CheckInDate < res.CheckOutDate &&
                   request.CheckOutDate > res.CheckInDate))
               .ToListAsync();
            var roomDtos = availableRooms.Adapt<IEnumerable<RoomDto>>();

            return roomDtos.Any()
                ? Result.Success(roomDtos)
                : Result.Failure<IEnumerable<RoomDto>>(RoomErrors.NoRoomsForHotel);
        }

        public async Task<Result<RoomRes>> UpdateRoomAsync(int id, RoomReq roomToUpdate, CancellationToken cancellationToken)
        {
            var existingRoom = await _context.Rooms
                .FirstOrDefaultAsync(r => r.RoomId == id && !r.IsDeleted, cancellationToken);

            if (existingRoom == null)
                return Result.Failure<RoomRes>(RoomErrors.RoomNotFound);

            if (Enum.TryParse(roomToUpdate.Type, true, out RoomType parsedType))
            {
                existingRoom.Type = parsedType; 
            }
            else
            {
                return Result.Failure<RoomRes>(RoomErrors.RoomTypeNotFound); 
            }

            roomToUpdate.Adapt(existingRoom);
            _context.Rooms.Update(existingRoom);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success(existingRoom.Adapt<RoomRes>());
        }



        public async Task<Result> SoftDeleteRoomAsync(int id, CancellationToken cancellationToken)
        {
            var room = await _context.Rooms
         .FirstOrDefaultAsync(r => r.RoomId == id, cancellationToken);

            if (room == null)
                return Result.Failure(RoomErrors.RoomNotFound);
            room.IsDeleted = true;
            room.DeletedAt = DateTime.UtcNow;
            room.Status = "Cancelled";

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result<bool>> DeleteRoomAsync(int id, CancellationToken cancellationToken)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomId == id, cancellationToken);
            if (room == null)
                return Result.Failure<bool>(RoomErrors.RoomNotFound);

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
