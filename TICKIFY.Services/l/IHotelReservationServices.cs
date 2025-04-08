using TICKIFY.Data.Entities;

namespace TICKIFY.Services.Abstracts
{
    public interface IHotelReservationServices
    {
        Task<IEnumerable<HotelReservations>> GetAllReservationsAsync();
        Task<HotelReservations> GetReservationByIdAsync(int id);
        Task<HotelReservations> CreateReservationAsync(HotelReservations reservation);
        Task<HotelReservations> UpdateReservationAsync(HotelReservations reservation);
        Task DeleteReservationAsync(int id);
    }
}

