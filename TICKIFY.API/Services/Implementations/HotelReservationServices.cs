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

        public async Task<Result<IEnumerable<ReservationRes>>> GetAllReservationsAsync(CancellationToken cancellationToken)
        {
            var reservations = await _context.HotelReservations
                .Include(r => r.Hotel)
                    .ThenInclude(h => h.Rooms)
                .Include(r => r.Hotel.Drivers)
                .ToListAsync(cancellationToken);

            if (!reservations.Any())
                return Result.Failure<IEnumerable<ReservationRes>>(HotelReservationErrors.ReservationNotFound);

            var reservationResList = reservations.Adapt<IEnumerable<ReservationRes>>();
            return Result.Success(reservationResList);
        }


        public async Task<Result<ReservationRes>> GetReservationByIdAsync(int id, CancellationToken cancellationToken)
        {
            var reservation = await _context.HotelReservations
                .Include(r => r.Hotel)
                    .ThenInclude(h => h.Rooms)
                .Include(r => r.Hotel.Drivers)
                .FirstOrDefaultAsync(r => r.HotelReservationId == id, cancellationToken);

            if (reservation == null)
                return Result.Failure<ReservationRes>(HotelReservationErrors.ReservationNotFound);

            var result = reservation.Adapt<ReservationRes>();
            return Result.Success(result);
        }

        public async Task<Result<ReservationRes>> CreateReservationAsync(ReservationReq reservationReq, CancellationToken cancellationToken)
        {
            var hotel = await _context.Hotels
                .Include(h => h.Drivers)
                .FirstOrDefaultAsync(h => h.HotelId == reservationReq.HotelId, cancellationToken);

            if (hotel == null)
                return Result.Failure<ReservationRes>(HotelErrors.HotelNotFound);
            if (!hotel.Drivers.Any(d => d.DriverId == reservationReq.DriverId))
                return Result.Failure<ReservationRes>(HotelErrors.NoDriversForHotel);

            var room = await _context.Rooms
                .FirstOrDefaultAsync(r => r.RoomId == reservationReq.RoomId && r.Status == "Available", cancellationToken);

            if (room == null)
                return Result.Failure<ReservationRes>(HotelReservationErrors.ReservationNotFound);

            var existingReservation = await _context.HotelReservations
                .Where(r => r.RoomId == reservationReq.RoomId)
                .Where(r => r.CheckInDate < reservationReq.CheckOutData && r.CheckOutDate > reservationReq.CheckInDate)
                .FirstOrDefaultAsync(cancellationToken);

            if (existingReservation != null)
                return Result.Failure<ReservationRes>(HotelReservationErrors.ReservationNotFound);

            var reservation = new HotelReservations
            {
                GuestName = reservationReq.GuestName,
                Email = reservationReq.Email,
                Phone = reservationReq.Phone,
                RoomId = reservationReq.RoomId,
                HotelId = reservationReq.HotelId,
                CheckInDate = reservationReq.CheckInDate.ToUniversalTime(),
                CheckOutDate = reservationReq.CheckOutData.ToUniversalTime(),
                Status = "Confirmed",
            };

            _context.HotelReservations.Add(reservation);

            room.Status = "Booked";
            room.DateOut = reservationReq.CheckOutData;
            _context.Rooms.Update(room);

            await _context.SaveChangesAsync(cancellationToken);

            var response = new ReservationRes
            {
                ReservationId = reservation.HotelReservationId,
                GuestName = reservation.GuestName,
                Email = reservation.Email,
                Phone = reservation.Phone,
                HotelId = reservation.HotelId,
                HotelName = reservationReq.HotelName,
                RoomId = reservation.RoomId,
                DriverId = reservationReq.DriverId, 
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                Status = reservation.Status
            };

            return Result.Success(response);
        }



        public async Task<Result<ReservationRes>> UpdateReservationAsync(int id, HotelReservationReq reservationReq, CancellationToken cancellationToken)
        {
            var existing = await _context.HotelReservations
                .Include(r => r.Room)
                .Include(r => r.Hotel)  
                .ThenInclude(h => h.Drivers)
               .FirstOrDefaultAsync(r => r.HotelReservationId == id, cancellationToken);

            if (existing == null)
                return Result.Failure<ReservationRes>(HotelReservationErrors.ReservationNotFound);

            existing.GuestName = reservationReq.GuestName;
            existing.Email = reservationReq.Email;
            existing.Phone = reservationReq.Phone;
            existing.CheckOutDate = reservationReq.CheckOutData.ToUniversalTime();  
            existing.RoomId = reservationReq.RoomId;

            _context.HotelReservations.Update(existing);

            await _context.SaveChangesAsync(cancellationToken);

            var reservationRes = new ReservationRes
            {
                ReservationId = existing.HotelReservationId,
                GuestName = existing.GuestName,
                Email = existing.Email,
                Phone = existing.Phone,
                HotelId = existing.HotelId, 
                HotelName = existing.Hotel.Name, 
                RoomId = existing.RoomId,
                DriverId = existing.Hotel.Drivers?.FirstOrDefault()?.DriverId ?? 0, 
                CheckInDate = existing.CheckInDate,
                CheckOutDate = existing.CheckOutDate,
                Status = existing.Status
            };

            return Result.Success(reservationRes);
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
