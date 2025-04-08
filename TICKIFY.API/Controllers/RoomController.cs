using Microsoft.AspNetCore.Mvc;
using TICKIFY.API.Contracts.Rooms;
using TICKIFY.API.Services.Abstracts;
using System.Threading;
using System.Threading.Tasks;
using TICKIFY.Data.Enums;

namespace TICKIFY.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomServices _roomServices;

        public RoomController(IRoomServices roomServices)
        {
            _roomServices = roomServices;
        }

        /// <summary>Get all rooms</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RoomRes>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRooms(CancellationToken cancellationToken)
        {
            var result = await _roomServices.GetAllRoomsAsync(cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : Problem(result.Errorr.Message);
        }

        /// <summary>Get rooms by hotel ID</summary>
        [HttpGet("hotel/{hotelId}")]
        [ProducesResponseType(typeof(IEnumerable<RoomRes>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoomsByHotel(int hotelId, CancellationToken cancellationToken)
        {
            var result = await _roomServices.GetRoomsByHotelAsync(hotelId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : Problem(result.Errorr.Message);
        }

        /// <summary>Get room by ID</summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RoomRes), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoomById(int id, CancellationToken cancellationToken)
        {
            var result = await _roomServices.GetRoomByIdAsync(id, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errorr.Message);
        }

        /// <summary>Get room price by ID</summary>
        [HttpGet("{id}/price")]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoomPrice(int id, CancellationToken cancellationToken)
        {
            var result = await _roomServices.GetRoomPriceByIdAsync(id, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errorr.Message);
        }

        /// <summary>Get room status by ID</summary>
        [HttpGet("{id}/status")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoomStatus(int id, [FromQuery] string status, CancellationToken cancellationToken)
        {
            // Convert the string status to enum
            if (Enum.TryParse<RoomStatus>(status, true, out var roomStatus))
            {
                var result = await _roomServices.GetRoomsByStatusAsync(roomStatus, cancellationToken);
                return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errorr.Message);
            }
            else
            {
                return BadRequest("Invalid room status provided.");
            }
        }

        /// <summary>Get room by hotel and room ID</summary>
        [HttpGet("hotel/{hotelId}/room/{roomId}")]
        [ProducesResponseType(typeof(RoomRes), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoomByHotelAndRoomId(int hotelId, int roomId, CancellationToken cancellationToken)
        {
            var result = await _roomServices.GetRoomByHotelAsync(hotelId, roomId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errorr.Message);
        }

        /// <summary>Create a new room</summary>
        [HttpPost]
        [ProducesResponseType(typeof(RoomRes), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateRoom([FromBody] RoomReq roomReq, CancellationToken cancellationToken)
        {
            // Ensure the request body is valid
            if (roomReq == null)
            {
                return BadRequest("Room data is required.");
            }

            var result = await _roomServices.CreateRoomAsync(roomReq, cancellationToken);

            return result.IsSuccess
                ? CreatedAtAction(nameof(GetRoomById), new { id = result.Value.RoomId }, result.Value)
                : Problem(result.Errorr.Message);
        }

        /// <summary>Update a room by ID</summary>
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

        /// <summary>Delete a room by ID</summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteRoom(int id, CancellationToken cancellationToken)
        {
            var result = await _roomServices.DeleteRoomAsync(id, cancellationToken);
            return result.IsSuccess ? NoContent() : NotFound(result.Errorr.Message);
        }
    }
}
//get rooms by hotel id statue
//make mapping to statue
//spicific room statue
//fix find room by price