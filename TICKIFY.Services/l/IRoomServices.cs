using TICKIFY.Data.Entities;
using TICKIFY.Data.Enums;

namespace TICKIFY.Services.Abstracts
{
    public interface IRoomServices
    {
        Task<IEnumerable<Rooms>> GetAllRoomsAsync();
        Task<Rooms> GetRoomByIdAsync(int id);
        Task<Rooms> GetRoomStatueAsync(RoomStatus statue);
        Task<Rooms> GetRoomTypeAsync(RoomType type);
        Task<Rooms> CreateRoomAsync(Rooms room);
        Task<Rooms> UpdateRoomAsync(Rooms room);
        Task DeleteRoomAsync(int id);
        Task<List<Rooms>> GetRoomsByHotelAsync(int hotelId);
    }
}
