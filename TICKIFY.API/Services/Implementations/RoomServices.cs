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

namespace TICKIFY.API.Services.Implementations
{
    public class RoomServices : IRoomServices
    {
        private readonly ApplicationDbContext _context;

        public RoomServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<RoomRes>>> GetAllRoomsAsync(CancellationToken cancellationToken)
        {
            var rooms = await _context.Rooms.ToListAsync(cancellationToken);

            if (!rooms.Any())
            {
                return Result.Failure<IEnumerable<RoomRes>>(RoomErrors.NoRoomsFound);
            }
            return Result.Success(rooms.Adapt<IEnumerable<RoomRes>>());
        }

        public async Task<Result<RoomRes>> GetRoomByIdAsync(int id, CancellationToken cancellationToken)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomId == id, cancellationToken);
            if (room == null)
                return Result.Failure<RoomRes>(RoomErrors.RoomNotFound);

            return Result.Success(room.Adapt<RoomRes>());
        }

        public async Task<Result<decimal>> GetRoomPriceByIdAsync(int id, CancellationToken cancellationToken)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomId == id, cancellationToken);
            return room == null
                ? Result.Failure<decimal>(RoomErrors.RoomNotFound)
                : Result.Success(room.PricePerNight);
        }

        public async Task<Result<RoomRes>> GetRoomByHotelAsync(int hotelId, int roomId, CancellationToken cancellationToken)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.HotelId == hotelId && r.RoomId == roomId, cancellationToken);
            return room == null
                ? Result.Failure<RoomRes>(RoomErrors.RoomNotFound)
                : Result.Success(room.Adapt<RoomRes>());
        }

        public async Task<Result<IEnumerable<HotelRoomsRes>>> GetRoomsByStatusAsync(RoomStatus status, CancellationToken cancellationToken)
        {
            var rooms = await _context.Rooms
                .Where(r => r.Status == status)
                .ToListAsync(cancellationToken);

            if (!rooms.Any())
            {
                return Result.Failure<IEnumerable<HotelRoomsRes>>(RoomErrors.RoomStatusNotFound);
            }

            var result = rooms.Adapt<IEnumerable<HotelRoomsRes>>()
                .Select(r =>
                {
                    // Convert enum to string using Enum.GetName
                    r.Status = Enum.GetName(typeof(RoomStatus), r.Status);
                    return r;
                })
                .ToList();

            return Result.Success<IEnumerable<HotelRoomsRes>>(result);
        }

        public async Task<Result<IEnumerable<RoomRes>>> GetRoomsByTypeAsync(RoomType type, CancellationToken cancellationToken)
        {
            var rooms = await _context.Rooms.Where(r => r.Type == type).ToListAsync();
            return !rooms.Any()
                ? Result.Failure<IEnumerable<RoomRes>>(RoomErrors.RoomTypeNotFound)
                : Result.Success(rooms.Adapt<IEnumerable<RoomRes>>());
        }

        public async Task<Result<IEnumerable<RoomRes>>> GetRoomsByHotelAsync(int hotelId, CancellationToken cancellationToken)
        {
            var rooms = await _context.Rooms
                .Where(r => r.HotelId == hotelId)
                .ToListAsync(cancellationToken);

            return !rooms.Any()
                ? Result.Failure<IEnumerable<RoomRes>>(RoomErrors.NoRoomsForHotel)
                : Result.Success(rooms.Adapt<IEnumerable<RoomRes>>());
        }

        public async Task<Result<RoomRes>> CreateRoomAsync(RoomReq roomReq, CancellationToken cancellationToken)
        {
            var room = roomReq.Adapt<Rooms>();
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success(room.Adapt<RoomRes>());
        }

        public async Task<Result<RoomRes>> UpdateRoomAsync(int id, RoomReq roomToUpdate, CancellationToken cancellationToken)
        {
            var existingRoom = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomId == id, cancellationToken);
            if (existingRoom == null)
                return Result.Failure<RoomRes>(RoomErrors.RoomNotFound);

            roomToUpdate.Adapt(existingRoom); 
            _context.Rooms.Update(existingRoom);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success(existingRoom.Adapt<RoomRes>());
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
