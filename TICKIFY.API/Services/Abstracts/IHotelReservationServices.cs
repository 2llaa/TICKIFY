using TICKIFY.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using TICKIFY.API.Contracts.HotelReservations;
using TICKIFY.API.Abstracts;

namespace TICKIFY.API.Services.Abstracts
{
    public interface IHotelReservationServices
    {
        Task<Result<IEnumerable<ReservationRes>>> GetAllReservationsAsync(CancellationToken cancellationToken);
        Task<Result<IEnumerable<HotelReservationRes>>> GetReservationsByHotelIdAsync(int hotelId, CancellationToken cancellationToken);
        // Task<Result<IEnumerable<HotelReservationRes>>> GetReservationsByUserIdAsync(int userId, CancellationToken cancellationToken);
        Task<Result<ReservationRes>> CreateReservationAsync(ReservationReq reservationReq, CancellationToken cancellationToken);
        Task<Result<ReservationRes>> GetReservationByIdAsync(int id, CancellationToken cancellationToken);
        Task<Result<ReservationRes>> UpdateReservationAsync(int id, HotelReservationReq reservationReq, CancellationToken cancellationToken);
        Task<Result> DeleteReservationAsync(int id, CancellationToken cancellationToken);
    }
}