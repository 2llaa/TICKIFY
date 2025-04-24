using Microsoft.AspNetCore.Mvc;
using TICKIFY.API.Contracts.Rooms;
using TICKIFY.API.Services.Abstracts;
using System.Threading;
using System.Threading.Tasks;
using TICKIFY.Data.Enums;
using TICKIFY.API.Services.Implementations;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;

namespace TICKIFY.API.Controllers.Admin
{
    [ApiController]
    [Route("api/Admin/[controller]")]
    [Authorize(Roles = "Admin")]

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

        //Create a new room (for admin)
        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] RoomReq roomReq, CancellationToken cancellationToken)
        {
            if (roomReq == null)
            {
                return BadRequest("Request data is required");
            }

            var result = await _roomServices.CreateRoomAsync(roomReq, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : Problem(result.Errorr.Message);

        }

        //Update a room by ID
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomReq roomReq, CancellationToken cancellationToken)
        {
            if (roomReq == null)
            {
                return BadRequest("Room data is required.");
            }
            var result = await _roomServices.UpdateRoomAsync(id, roomReq, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : Problem(result.Errorr.Message);
        }

        //Delete a room by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id, CancellationToken cancellationToken)
        {
            var result = await _roomServices.DeleteRoomAsync(id, cancellationToken);
            return result.IsSuccess ? NoContent() : NotFound(result.Errorr.Message);
        }

        //soft delete a room by ID
        [HttpDelete("soft/{id}")]
        public async Task<IActionResult> SoftDeleteRoom(int id, CancellationToken cancellationToken)
        {
            var result = await _roomServices.SoftDeleteRoomAsync(id, cancellationToken);
            return result.IsSuccess ? NoContent() : NotFound(result.Errorr.Message);
        }
    }
}
