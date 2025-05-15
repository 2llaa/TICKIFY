using Tickfy.Contracts.Rooms;

namespace Tickfy.Services.Rooms;

public interface IRoomService
{
    Task<Result<IEnumerable<RoomReservationsDatesResponse>>> GetRoomReservationsDatesAsync(int HotelId,int Id, CancellationToken cancellationToken);
    Task<Result<IEnumerable<RoomResponse>>>SelectRoomAsync(SelectRoomRequest request,int HotelId, CancellationToken cancellation);
}
