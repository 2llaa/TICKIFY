using Mapster;
using Microsoft.AspNetCore.Mvc;
using TICKIFY.API.Contracts.HotelReservations;
using TICKIFY.API.Services.Abstracts;
using TICKIFY.Data.Entities;

namespace TICKIFY.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelReservationController : ControllerBase
    {
        private readonly IHotelReservationServices _reservationServices;

        public HotelReservationController(IHotelReservationServices reservationServices)
        {
            _reservationServices = reservationServices;
        }

        /// <summary>Get all hotel reservations including hotel name, room type, driver car, and price</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<HotelReservationRes>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllReservations(CancellationToken cancellationToken)
        {
            var result = await _reservationServices.GetAllReservationsAsync(cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : Problem(result.Errorr.Message);
        }

        /// <summary>Get a hotel reservation by ID</summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(HotelReservationRes), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReservationById(int id, CancellationToken cancellationToken)
        {
            var result = await _reservationServices.GetReservationByIdAsync(id, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errorr.Message);
        }

        /// <summary>Create a new hotel reservation</summary>
        [HttpPost]
        [ProducesResponseType(typeof(HotelReservationRes), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateReservation([FromBody] HotelReservationReq reservationReq, CancellationToken cancellationToken)
        {
            if (reservationReq == null)
                return BadRequest("Reservation data is required.");

            var result = await _reservationServices.CreateReservationAsync(reservationReq, cancellationToken);

            return result.IsSuccess
                ? CreatedAtAction(nameof(GetReservationById), new { id = result.Value.HotelReservationId }, result.Value)
                : Problem(result.Errorr.Message);
        }

        /// <summary>Update an existing hotel reservation</summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(HotelReservationRes), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateReservation(int id, [FromBody] HotelReservationReq reservationReq, CancellationToken cancellationToken)
        {
            var result = await _reservationServices.UpdateReservationAsync(id, reservationReq, new HotelReservations(), cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errorr.Message);
        }

        /// <summary>Delete a hotel reservation</summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteReservation(int id, CancellationToken cancellationToken)
        {
            var result = await _reservationServices.GetReservationByIdAsync(id, cancellationToken);
            if (!result.IsSuccess) return NotFound(result.Errorr.Message);

            await _reservationServices.DeleteReservationAsync(id, cancellationToken);
            return NoContent();
        }

        /// <summary>Get reservations by hotel ID</summary>
        [HttpGet("by-hotel/{hotelId}")]
        [ProducesResponseType(typeof(IEnumerable<HotelReservationRes>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReservationsByHotelId(int hotelId, CancellationToken cancellationToken)
        {
            var result = await _reservationServices.GetReservationsByHotelIdAsync(hotelId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errorr.Message);
        }

        /// <summary>Get reservations by user ID</summary>
        //[HttpGet("by-user/{userId}")]
        //[ProducesResponseType(typeof(IEnumerable<HotelReservationRes>), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GetReservationsByUserId(int userId, CancellationToken cancellationToken)
        //{
        //    var result = await _reservationServices.GetReservationsByUserIdAsync(userId, cancellationToken);
        //    return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errorr.Message);
        //}
    }
}
