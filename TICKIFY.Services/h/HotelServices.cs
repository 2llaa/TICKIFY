using Microsoft.EntityFrameworkCore;
using TICKIFY.Data.Entities;
using TICKIFY.Data.Enums;
using TICKIFY.Services.Abstracts;
using TICKIFY.Infrastracture.Persistence.Data;

namespace TICKIFY.Services.Implementations
{
    public class HotelServices : IHotelServices
    {
        private readonly ApplicationDbContext _context;

        public HotelServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hotels>> GetAllHotelsAsync()
        {
            return await _context.Hotels
                .Include(h => h.Drivers)
                .Include(h => h.Rooms)
                .Include(h => h.HotelReservations)
                .ToListAsync();
        }

        public async Task<Hotels> GetHotelByIdAsync(int id)
        {
            return await _context.Hotels
                .Include(h => h.Drivers)
                .Include(h => h.Rooms)
                .Include(h => h.HotelReservations)
                .FirstOrDefaultAsync(h => h.HotelId == id);
        }

        public async Task<Hotels> GetHotelByNameAsync(HotelName name)
        {
            return await _context.Hotels
                .Include(h => h.Drivers)
                .Include(h => h.Rooms)
                .Include(h => h.HotelReservations)
                .FirstOrDefaultAsync(h => h.Name == name);
        }

        public async Task<Hotels> CreateHotelAsync(Hotels hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task<Hotels> UpdateHotelAsync(Hotels hotel)
        {
            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task DeleteHotelAsync(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Drivers>> GetHotelDriversAsync(int hotelId)
        {
            return await _context.Drivers
                                 .Where(d => d.HotelId == hotelId)
                                 .ToListAsync();
        }

    }
}
