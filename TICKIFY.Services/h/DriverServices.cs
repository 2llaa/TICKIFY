//using Microsoft.EntityFrameworkCore;
//using TICKIFY.Data.Entities;
//using TICKIFY.Infrastracture.Persistence.Data;
//using TICKIFY.Services.Abstracts;

//namespace TICKIFY.Services.Implementations
//{
//    public class DriverServices : IDriverServices
//    {
//        private readonly ApplicationDbContext _context;

//        public DriverServices(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<Drivers>> GetAllDriversAsync()
//        {
//            return await _context.Drivers
//                .Include(d => d.Hotel)
//                .ToListAsync();
//        }

//        public async Task<Drivers> GetDriverByIdAsync(int id)
//        {
//            return await _context.Drivers
//                .Include(d => d.Hotel)
//                .FirstOrDefaultAsync(d => d.DriverId == id);
//        }

//        public async Task<Drivers> CreateDriverAsync(Drivers driver)
//        {
//            _context.Drivers.Add(driver);
//            await _context.SaveChangesAsync();
//            return driver;
//        }

//        public async Task<Drivers> UpdateDriverAsync(Drivers driver)
//        {
//            _context.Drivers.Update(driver);
//            await _context.SaveChangesAsync();
//            return driver;
//        }

//        public async Task<IEnumerable<Drivers>> GetDriversByHotelIdAsync(int hotelId)
//        {
//            return await _context.Drivers
//                .Where(d => d.HotelId == hotelId)
//                .Include(d => d.Hotel)
//                .ToListAsync();
//        }

//    }
//}
