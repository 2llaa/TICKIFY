using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tickfy.Contracts.HotelReservations;
using Tickfy.Services.HotelReservations;

namespace Tickfy.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class HotelReservationController(IHotelReservationService hotelReservationService) : ControllerBase
{
    private readonly IHotelReservationService _hotelReservationService = hotelReservationService;


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var result = await _hotelReservationService.GetByIdAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result) : NotFound(result.Error);
    }


    [HttpPost("Reserve")]
    public async Task<IActionResult> Reserve([FromBody] HotelReservationRequest request, CancellationToken cancellationToken)
    {
        var result = await _hotelReservationService.ReserveAsync(request, cancellationToken);

        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value) :
            BadRequest(result.Error);

    }
}