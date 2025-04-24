using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TICKIFY.API.Abstracts;
using TICKIFY.API.Contracts.HotelReservations;
using TICKIFY.API.Services.Abstracts;
using TICKIFY.Data.Entities;

namespace TICKIFY.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]

    public class HotelReservationController : ControllerBase
    {
        private readonly IHotelReservationServices _reservationServices;

        public HotelReservationController(IHotelReservationServices reservationServices)
        {
            _reservationServices = reservationServices;
        }


        //Create a reservation

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

    }
}
