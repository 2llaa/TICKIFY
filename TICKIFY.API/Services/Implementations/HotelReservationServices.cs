using Microsoft.EntityFrameworkCore;
using TICKIFY.API.Services.Abstracts;
using TICKIFY.Data.Entities;
using TICKIFY.API.Contracts.HotelReservations;
using MapsterMapper;
using TICKIFY.API.Persistence.Data;
using TICKIFY.API.Abstracts;
using TICKIFY.API.Errors.Reservation;
using TICKIFY.API.Errors.Hotel;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Mapster;

namespace TICKIFY.API.Services.Implementations
{
    public class HotelReservationServices : IHotelReservationServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HotelReservationServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<HotelReservationRes>>> GetAllReservationsAsync(CancellationToken cancellationToken)
        {
            var reservations = await _context.HotelReservations
                .Include(r => r.Hotel)
                    .ThenInclude(h => h.Rooms)
                .Include(r => r.Hotel.Drivers)
                .ToListAsync(cancellationToken);

            if (!reservations.Any())
                return Result.Failure<IEnumerable<HotelReservationRes>>(HotelReservationErrors.ReservationNotFound);

            var reservationResList = reservations.Adapt<IEnumerable<HotelReservationRes>>();
            return Result.Success(reservationResList);
        }

        public async Task<Result<HotelReservationRes>> GetReservationByIdAsync(int id, CancellationToken cancellationToken)
        {
            var reservation = await _context.HotelReservations
                .Include(r => r.Hotel)
                    .ThenInclude(h => h.Rooms)
                .Include(r => r.Hotel.Drivers)
                .FirstOrDefaultAsync(r => r.HotelReservationId == id, cancellationToken);

            if (reservation == null)
                return Result.Failure<HotelReservationRes>(HotelReservationErrors.ReservationNotFound);

            var result = reservation.Adapt<HotelReservationRes>();
            return Result.Success(result);
        }

        public async Task<Result<HotelReservationRes>> CreateReservationAsync(HotelReservationReq reservationReq, CancellationToken cancellationToken)
        {
            var hotelExists = await _context.Hotels.AnyAsync(h => h.HotelId == reservationReq.HotelId, cancellationToken);

            if (!hotelExists)
                return Result.Failure<HotelReservationRes>(HotelErrors.HotelNotFound);

            var reservation = new HotelReservations
            {
                CheckInDate = reservationReq.CheckInDate,
                CheckOutDate = reservationReq.CheckOutDate,
                HotelId = reservationReq.HotelId,
               // UserId = reservationReq.UserId // Assuming user ID is part of the request
            };

            _context.HotelReservations.Add(reservation);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(reservation.Adapt<HotelReservationRes>());
        }

        public async Task<Result<HotelReservationRes>> UpdateReservationAsync(int id, HotelReservationReq reservationReq, HotelReservations reservation, CancellationToken cancellationToken)
        {
            var existing = await _context.HotelReservations
                .FirstOrDefaultAsync(r => r.HotelReservationId == id, cancellationToken);

            if (existing is null)
                return Result.Failure<HotelReservationRes>(HotelReservationErrors.ReservationNotFound);

            reservation.Adapt(existing);

            _context.HotelReservations.Update(existing);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(existing.Adapt<HotelReservationRes>());
        }

        public async Task<Result> DeleteReservationAsync(int id, CancellationToken cancellationToken)
        {
            var reservation = await _context.HotelReservations
                .FirstOrDefaultAsync(r => r.HotelReservationId == id, cancellationToken);

            if (reservation == null)
                return Result.Failure(HotelReservationErrors.ReservationNotFound);

            _context.HotelReservations.Remove(reservation);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result<IEnumerable<HotelReservationRes>>> GetReservationsByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
        {
            var reservations = await _context.HotelReservations
                .Include(r => r.Hotel)
                    .ThenInclude(h => h.Rooms)
                .Include(r => r.Hotel.Drivers)
                .Where(r => r.HotelId == hotelId)
                .ToListAsync(cancellationToken);

            if (!reservations.Any())
                return Result.Failure<IEnumerable<HotelReservationRes>>(HotelReservationErrors.ReservationNotFound);

            var result = reservations.Adapt<IEnumerable<HotelReservationRes>>();
            return Result.Success(result);
        }

        //public async Task<Result<IEnumerable<HotelReservationRes>>> GetReservationsByUserIdAsync(int userId, CancellationToken cancellationToken)
        //{
        //    var reservations = await _context.HotelReservations
        //        .Include(r => r.Hotel)
        //            .ThenInclude(h => h.Rooms)
        //        .Include(r => r.Hotel.Drivers)
        //        .Where(r => r.UserId == userId)
        //        .ToListAsync(cancellationToken);

        //    if (!reservations.Any())
        //        return Result.Failure<IEnumerable<HotelReservationRes>>(HotelReservationErrors.ReservationNotFound);

        //    var result = reservations.Adapt<IEnumerable<HotelReservationRes>>();
        //    return Result.Success(result);
        //}
    }
}
