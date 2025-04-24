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

        public async Task CheckExpiredReservations(CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var expiredReservations = await _context.HotelReservations
                .Include(r => r.Room)
                .Where(r => !r.IsDeleted &&
                       r.Status == "Confirmed" &&
                       r.CheckOutDate < now)
                .ToListAsync(cancellationToken);

            foreach (var reservation in expiredReservations)
            {
                reservation.Status = "Completed";
                if (reservation.Room != null)
                {
                    reservation.Room.Status = "Available";
                }
            }

            if (expiredReservations.Any())
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<Result<IEnumerable<ReservationRes>>> GetAllReservationsAsync(CancellationToken cancellationToken)
        {
            await CheckExpiredReservations(cancellationToken);

            var reservations = await _context.HotelReservations
                .Include(r => r.Hotel)
                .Include(r => r.Room)
                .Include(r => r.Driver)
                .ToListAsync(cancellationToken);

            if (!reservations.Any())
                return Result.Failure<IEnumerable<ReservationRes>>(HotelReservationErrors.ReservationNotFound);

            var reservationResList = reservations.Select(r => new ReservationRes
            {
                ReservationId = r.HotelReservationId,
                HotelId = r.HotelId,
                HotelName = r.Hotel?.Name,
                RoomId = r.RoomId,
                RoomNumber = r.Room.RoomNumber,
                DriverId = r.DriverId,
                DriverName = r.Driver?.DriverName,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                Status = r.Status,
                TotalPrice = (r.Room?.PricePerNight ?? 0) * (int)(r.CheckOutDate - r.CheckInDate).TotalDays + (r.Driver?.Price ?? 0)
            });

            return Result.Success(reservationResList);
        }

        public async Task<Result<ReservationRes>> GetReservationByIdAsync(int id, CancellationToken cancellationToken)
        {
            await CheckExpiredReservations(cancellationToken);

            var reservation = await _context.HotelReservations
                .Include(r => r.Hotel)
                .Include(r => r.Room)
                .Include(r => r.Driver)
                .FirstOrDefaultAsync(r => r.HotelReservationId == id, cancellationToken);

            if (reservation == null)
                return Result.Failure<ReservationRes>(HotelReservationErrors.ReservationNotFound);

            var result = new ReservationRes
            {
                ReservationId = reservation.HotelReservationId,
                HotelId = reservation.HotelId,
                HotelName = reservation.Hotel?.Name,
                RoomId = reservation.RoomId,
                RoomNumber = reservation.Room.RoomNumber,
                DriverId = reservation.DriverId,
                DriverName = reservation.Driver?.DriverName,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                Status = reservation.Status,
                TotalPrice = (reservation.Room?.PricePerNight ?? 0) * (int)(reservation.CheckOutDate - reservation.CheckInDate).TotalDays +
                            (reservation.Driver?.Price ?? 0)
            };

            return Result.Success(result);
        }

        public async Task<Result<ReservationRes>> CreateReservationAsync(ReservationReq reservationReq, CancellationToken cancellationToken)
        {
            var hotel = await _context.Hotels
                .Include(h => h.Drivers)
                .FirstOrDefaultAsync(h => h.HotelId == reservationReq.HotelId, cancellationToken);

            if (hotel == null)
                return Result.Failure<ReservationRes>(HotelErrors.HotelNotFound);

            var driver = await _context.Drivers
                .FirstOrDefaultAsync(d => d.DriverId == reservationReq.DriverId && d.HotelId == reservationReq.HotelId, cancellationToken);

            if (driver == null)
                return Result.Failure<ReservationRes>(HotelErrors.NoDriversForHotel);

            var room = await _context.Rooms
                .FirstOrDefaultAsync(r => r.RoomId == reservationReq.RoomId && r.Status == "Available", cancellationToken);

            if (room == null)
                return Result.Failure<ReservationRes>(HotelReservationErrors.RoomNotAvailable);

            var existingReservation = await _context.HotelReservations
                .Where(r => r.RoomId == reservationReq.RoomId)
                .Where(r => r.CheckInDate < reservationReq.CheckOutData && r.CheckOutDate > reservationReq.CheckInDate)
                .FirstOrDefaultAsync(cancellationToken);

            if (existingReservation != null)
                return Result.Failure<ReservationRes>(HotelReservationErrors.RoomAlreadyBooked);

            var reservation = new HotelReservations
            {
                DriverId = reservationReq.DriverId,
                RoomId = reservationReq.RoomId,
                HotelId = reservationReq.HotelId,
                CheckInDate = reservationReq.CheckInDate.ToUniversalTime(),
                CheckOutDate = reservationReq.CheckOutData.ToUniversalTime(),
                Status = "Confirmed",
            };

            _context.HotelReservations.Add(reservation);
            room.Status = "Booked";
            await _context.SaveChangesAsync(cancellationToken);

            var response = new ReservationRes
            {
                ReservationId = reservation.HotelReservationId,
                HotelId = reservation.HotelId,
                HotelName = hotel.Name,
                RoomId = reservation.RoomId,
                RoomNumber = room.RoomNumber,
                DriverId = reservation.DriverId,
                DriverName = driver.DriverName,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                Status = reservation.Status,
                TotalPrice = (room.PricePerNight * (int)(reservation.CheckOutDate - reservation.CheckInDate).TotalDays) + driver.Price
            };

            return Result.Success(response);
        }

        public async Task<Result<ReservationRes>> UpdateReservationAsync(int id, ReservationReq reservationReq, CancellationToken cancellationToken)
        {
            var existing = await _context.HotelReservations
                .Include(r => r.Room)
                .Include(r => r.Driver)
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(r => r.HotelReservationId == id, cancellationToken);

            if (existing == null)
                return Result.Failure<ReservationRes>(HotelReservationErrors.ReservationNotFound);

            if (reservationReq.DriverId != existing.DriverId)
            {
                var newDriver = await _context.Drivers
                    .FirstOrDefaultAsync(d => d.DriverId == reservationReq.DriverId && d.HotelId == existing.HotelId, cancellationToken);

                if (newDriver == null)
                    return Result.Failure<ReservationRes>(HotelErrors.NoDriversForHotel);
            }

            existing.DriverId = reservationReq.DriverId;
            existing.CheckOutDate = reservationReq.CheckOutData.ToUniversalTime();
            existing.RoomId = reservationReq.RoomId;

            await _context.SaveChangesAsync(cancellationToken);
            await _context.Entry(existing).ReloadAsync(cancellationToken);

            var reservationRes = new ReservationRes
            {
                ReservationId = existing.HotelReservationId,
                HotelId = existing.HotelId,
                HotelName = existing.Hotel?.Name,
                RoomId = existing.RoomId,
                RoomNumber = existing.Room.RoomNumber,
                DriverId = existing.DriverId,
                DriverName = existing.Driver?.DriverName,
                CheckInDate = existing.CheckInDate,
                CheckOutDate = existing.CheckOutDate,
                Status = existing.Status,
                TotalPrice = (existing.Room?.PricePerNight ?? 0) * (int)(existing.CheckOutDate - existing.CheckInDate).TotalDays +
                           (existing.Driver?.Price ?? 0)
            };

            return Result.Success(reservationRes);
        }

        public async Task<Result> DeleteReservationAsync(int id, CancellationToken cancellationToken)
        {
            var reservation = await _context.HotelReservations
         .Include(r => r.ReservationDetails)
         .FirstOrDefaultAsync(r => r.HotelReservationId == id, cancellationToken);

            if (reservation == null)
                return Result.Failure(HotelReservationErrors.NoReservationsFound);

            foreach (var detail in reservation.ReservationDetails)
            {
                detail.IsDeleted = true;
                detail.DeletedAt = DateTime.UtcNow;
            }

            reservation.IsDeleted = true;
            reservation.DeletedAt = DateTime.UtcNow;
            reservation.Status = "Cancelled";

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result<IEnumerable<ReservationRes>>> GetReservationsByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
        {
            await CheckExpiredReservations(cancellationToken);

            var reservations = await _context.HotelReservations
                .Include(r => r.Hotel)
                .Include(r => r.Room)
                .Include(r => r.Driver)
                .Where(r => r.HotelId == hotelId)
                .ToListAsync(cancellationToken);

            if (!reservations.Any())
                return Result.Failure<IEnumerable<ReservationRes>>(HotelReservationErrors.ReservationNotFound);

            var result = reservations.Select(r => new ReservationRes
            {
                ReservationId = r.HotelReservationId,
                HotelId = r.HotelId,
                RoomId = r.RoomId,
                RoomNumber = r.Room.RoomNumber,
                DriverId = r.DriverId,
                DriverName = r.Driver?.DriverName,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                Status = r.Status,
                TotalPrice = (r.Room?.PricePerNight ?? 0) * (int)(r.CheckOutDate - r.CheckInDate).TotalDays +
                            (r.Driver?.Price ?? 0)
            });

            return Result.Success(result);
        }
    }
}