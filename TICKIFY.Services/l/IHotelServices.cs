using TICKIFY.Data.Entities;
using TICKIFY.Data.Enums;

namespace TICKIFY.Services.Abstracts
{
    public interface IHotelServices
    {
        Task<IEnumerable<Hotels>> GetAllHotelsAsync();
        Task<Hotels> GetHotelByIdAsync(int id);
        Task<Hotels> GetHotelByNameAsync(HotelName name);

        Task<Hotels> CreateHotelAsync(Hotels hotel);
        Task<Hotels> UpdateHotelAsync(Hotels hotel);
        Task DeleteHotelAsync(int id);
        Task<List<Drivers>> GetHotelDriversAsync(int hotelId);

    }
}
