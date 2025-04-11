using TICKIFY.API.Contracts.Drivers;
using TICKIFY.API.Contracts.hotels;
using TICKIFY.Data.Entities;
using TICKIFY.Data.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using TICKIFY.API.Contracts.hotels;
using TICKIFY.API.Abstracts;
using TICKIFY.API.Contracts.Requests;
using TICKIFY.API.Contracts.Rooms;
using TICKIFY.API.Contracts.Hotels;

namespace TICKIFY.API.Services.Abstracts
{
    public interface IHotelServices
    {
        Task<Result<HotelByIdRes>> GetHotelByIdAsync(int id, CancellationToken cancellationToken);
        Task<Result<HotelRes>> CreateHotelAsync(HotelReq hotelReq, CancellationToken cancellationToken);
        Task<Result<HotelRes>> UpdateHotelAsync(int id, HotelReq hotelReq, CancellationToken cancellationToken);
        Task<Result<bool>> DeleteHotelAsync(int id, CancellationToken cancellationToken);
        Task<Result<IEnumerable<DriverRes>>> GetHotelDriversAsync(int hotelId, CancellationToken cancellationToken);
        Task<Result<IEnumerable<SearchHotelRes>>> GetHotelsByNameAsync(string name, CancellationToken cancellationToken);
        Task<Result<IEnumerable<HotelByIdRoomRes>>> GetHotelRoomsAsync(int hotelId, CancellationToken cancellationToken);
        Task<Result<IEnumerable<HotelRes>>> GetHotelsByLocationAsync(string location, CancellationToken cancellationToken);
        Task<Result<IEnumerable<HotelRes>>> GetHotelsByStarRatingAsync(int stars, CancellationToken cancellationToken);
        Task<Result<IEnumerable<HotelRes>>> GetHotelsByLocationAndStarRatingAsync(string location, int? starRating, CancellationToken cancellationToken);
        string NormalizeHotelName(string input);
        string NormalizeText(string input);

    }
}
