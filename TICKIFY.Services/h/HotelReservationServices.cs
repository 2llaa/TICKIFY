using Microsoft.EntityFrameworkCore;
using TICKIFY.Data;
using TICKIFY.Data.Entities;
using TICKIFY.Infrastracture.Persistence.Data;
using TICKIFY.Services.Abstracts;

namespace TICKIFY.Services.Implementations
{
    public class HotelReservationServices : IHotelReservationServices
    {
        private readonly ApplicationDbContext _context;

        public HotelReservationServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HotelReservations>> GetAllReservationsAsync()
        {
            return await _context.HotelReservations
                .Include(r => r.Hotel)
                .ToListAsync();
        }

        public async Task<HotelReservations> GetReservationByIdAsync(int id)
        {
            return await _context.HotelReservations
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(r => r.HotelReservationId == id);
        }

        public async Task<HotelReservations> CreateReservationAsync(HotelReservations reservation)
        {
            _context.HotelReservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task<HotelReservations> UpdateReservationAsync(HotelReservations reservation)
        {
            _context.HotelReservations.Update(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task DeleteReservationAsync(int id)
        {
            var reservation = await _context.HotelReservations.FindAsync(id);
            if (reservation != null)
            {
                _context.HotelReservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
