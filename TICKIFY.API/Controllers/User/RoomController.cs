using Microsoft.AspNetCore.Mvc;
using TICKIFY.API.Contracts.Rooms;
using TICKIFY.API.Services.Abstracts;
using System.Threading;
using System.Threading.Tasks;
using TICKIFY.Data.Enums;
using TICKIFY.API.Services.Implementations;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;

namespace TICKIFY.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]

    public class RoomController : ControllerBase
    {
        private readonly IRoomServices _roomServices;

        public RoomController(IRoomServices roomServices)
        {
            _roomServices = roomServices;
        }

        //Get room by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(int id, CancellationToken cancellationToken)
        {
            var result = await _roomServices.GetRoomByIdAsync(id, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errorr.Message);
        }

        //Get room price by ID
        [HttpGet("{id}/price")]
        public async Task<IActionResult> GetRoomPrice(int id, CancellationToken cancellationToken)
        {
            var result = await _roomServices.GetRoomPriceByIdAsync(id, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errorr.Message);
        }

        //Get rooms by type
        [HttpGet("type/{type}")]
        public async Task<IActionResult> GetRoomsByType(string type, CancellationToken cancellationToken)
        {
            var result = await _roomServices.GetRoomsByTypeAsync(type, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errorr.Message);
        }

        //Get available rooms by date and hotel ID
        [HttpPost("available-rooms")]
        public async Task<IActionResult> GetAvailableRooms([FromBody] RoomAvailabilityRequest request)
        {
            var result = await _roomServices.GetAvailableRoomsAsync(request);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errorr);
        }

        //Get room by hotel and room ID
        [HttpGet("hotel/{hotelId}/room/{roomId}")]
        public async Task<IActionResult> GetRoomByHotelAndRoomId(int hotelId, int roomId, CancellationToken cancellationToken)
        {
            var result = await _roomServices.GetRoomByHotelAsync(hotelId, roomId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errorr.Message);
        }

    }
}
