using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tickfy.Contracts.Rooms;
using Tickfy.Services.Rooms;

namespace Tickfy.Controllers;
[Route("api/Hotels/{HotelId}/[controller]")]
[ApiController]
public class RoomsController(IRoomService roomService) : ControllerBase
{
    private readonly IRoomService _roomService = roomService;


    [HttpGet("RoomReservationsDates,{Id}")]
    public async Task<IActionResult> GetRoomReservationsDates([FromRoute]int HotelId,int Id,CancellationToken cancellationToken)
    {
        var result = await _roomService.GetRoomReservationsDatesAsync(HotelId, Id, cancellationToken);

        return result.IsSuccess?
            Ok(result.Value):
            NotFound(result.Error);
    }

    [HttpGet("select")]
    public async Task<IActionResult> SelectRoom([FromQuery] SelectRoomRequest request,[FromRoute] int HotelId,CancellationToken cancellationToken)
    {
        var result = await _roomService.SelectRoomAsync(request, HotelId,cancellationToken);

        return result.IsSuccess ?Ok(result.Value) : NotFound(result.Error); 
    }
}
