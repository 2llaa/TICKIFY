using TICKIFY.API.Abstracts;
using TICKIFY.API.Contracts.Rooms;
using TICKIFY.Data.Entities;
using TICKIFY.Data.Enums;

namespace TICKIFY.API.Services.Abstracts
{
    public interface IRoomServices
    {
        Task<Result<IEnumerable<RoomRes>>> GetAllRoomsAsync(CancellationToken cancellationToken);
        Task<Result<RoomRes>> GetRoomByIdAsync(int id, CancellationToken cancellationToken);
        Task<Result<decimal>> GetRoomPriceByIdAsync(int id, CancellationToken cancellationToken);
        Task<Result<RoomRes>> GetRoomByHotelAsync(int hotelId, int roomId, CancellationToken cancellationToken);
        Task<Result<string>> GetRoomStatusAtTimeAsync(int roomId, DateTime dateTime, CancellationToken cancellationToken);
        Task<Result<IEnumerable<RoomRes>>> GetAvailableRoomsForDateAsync(DateTime dateTime, int hotelId, CancellationToken cancellationToken);
        Task<Result<IEnumerable<RoomDto>>> GetAvailableRoomsAsync(RoomAvailabilityRequest request);

        Task<Result<IEnumerable<RoomRes>>> GetRoomsByTypeAsync(string type, CancellationToken cancellationToken);
        Task<Result<IEnumerable<RoomRes>>> GetRoomsByHotelAsync(int hotelId, CancellationToken cancellationToken);
        Task<Result<HotelRoomsRes>> CreateRoomAsync(RoomReq roomReq, CancellationToken cancellationToken);
        Task<Result<RoomRes>> UpdateRoomAsync(int id, RoomReq roomToUpdate, CancellationToken cancellationToken);
        Task<Result<bool>> DeleteRoomAsync(int id, CancellationToken cancellationToken);
    }
}
