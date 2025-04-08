using TICKIFY.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using TICKIFY.API.Contracts.HotelReservations;
using TICKIFY.API.Abstracts;

namespace TICKIFY.API.Services.Abstracts
{
    public interface IHotelReservationServices
    {
        Task<Result<IEnumerable<HotelReservationRes>>> GetAllReservationsAsync(CancellationToken cancellationToken);
        Task<Result<IEnumerable<HotelReservationRes>>> GetReservationsByHotelIdAsync(int hotelId, CancellationToken cancellationToken);
       // Task<Result<IEnumerable<HotelReservationRes>>> GetReservationsByUserIdAsync(int userId, CancellationToken cancellationToken);

        Task<Result<HotelReservationRes>> GetReservationByIdAsync(int id, CancellationToken cancellationToken);
        Task<Result<HotelReservationRes>> CreateReservationAsync(HotelReservationReq reservationReq, CancellationToken cancellationToken);
        Task<Result<HotelReservationRes>> UpdateReservationAsync(int id, HotelReservationReq reservationReq, HotelReservations reservation, CancellationToken cancellationToken);
        Task<Result> DeleteReservationAsync(int id, CancellationToken cancellationToken);
    }
}