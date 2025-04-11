using Mapster;
using Microsoft.AspNetCore.Mvc;
using TICKIFY.API.Abstracts;
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

        //Get all hotel reservations
        [HttpGet("get-all-reservations")]
        public async Task<IActionResult> GetAllReservations(CancellationToken cancellationToken)
        {
            var result = await _reservationServices.GetAllReservationsAsync(cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : Problem(result.Errorr.Message);
        }

        //Create a new Room reservation

        [HttpPost("reserve")]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationReq request, CancellationToken cancellationToken)
        {
            var result = await _reservationServices.CreateReservationAsync(request, cancellationToken);

            if (result.IsSuccess)
                return Ok(result.Value);

            return BadRequest(result.Errorr);
        }

        //Get a hotel reservations by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(int id, CancellationToken cancellationToken)
        {
            var result = await _reservationServices.GetReservationByIdAsync(id, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errorr.Message);
        }



        //Update an existing hotel reservation
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, [FromBody] HotelReservationReq reservationReq, CancellationToken cancellationToken)
        {
            var result = await _reservationServices.UpdateReservationAsync(id, reservationReq, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errorr.Message);
        }

        //Delete a hotel reservation
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id, CancellationToken cancellationToken)
        {
            var result = await _reservationServices.GetReservationByIdAsync(id, cancellationToken);
            if (!result.IsSuccess) return NotFound(result.Errorr.Message);

            await _reservationServices.DeleteReservationAsync(id, cancellationToken);
            return NoContent();
        }

    }
}
