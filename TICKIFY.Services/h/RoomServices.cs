using Microsoft.EntityFrameworkCore;
using TICKIFY.Data;
using TICKIFY.Data.Entities;
using TICKIFY.Data.Enums;
using TICKIFY.Infrastracture.Persistence.Data;
using TICKIFY.Services.Abstracts;

namespace TICKIFY.Services.Implementations
{
    public class RoomServices : IRoomServices
    {
        private readonly ApplicationDbContext _context;

        public RoomServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rooms>> GetAllRoomsAsync()
        {
            return await _context.Rooms.Include(r => r.Hotel).ToListAsync();
        }

        public async Task<Rooms> GetRoomByIdAsync(int id)
        {
            return await _context.Rooms.Include(r => r.Hotel)
                                       .FirstOrDefaultAsync(r => r.RoomId == id);
        }

        public async Task<Rooms> GetRoomStatueAsync(RoomStatus status)
        {
            return await _context.Rooms.FirstOrDefaultAsync(r => r.Status == status);
        }

        public async Task<Rooms> GetRoomTypeAsync(RoomType type)
        {
            return await _context.Rooms.FirstOrDefaultAsync(r => r.Type == type);
        }

        public async Task<Rooms> CreateRoomAsync(Rooms room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<Rooms> UpdateRoomAsync(Rooms room)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task DeleteRoomAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Rooms>> GetRoomsByHotelAsync(int hotelId)
        {
            return await _context.Rooms
                                 .Where(r => r.HotelId == hotelId)
                                 .ToListAsync();
        }
    }
}
