using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tickfy.Controllers;
[Route("api/{flightID}/{classId}/[controller]")]
[ApiController]
[Authorize]
public class FlightReservationsController(IFlightReservationService flightReservationService) : ControllerBase
{
    private readonly IFlightReservationService _flightReservationService = flightReservationService;

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var flightReservation = await _flightReservationService.GetAsync(id,cancellationToken);

        return (flightReservation is null) ? NotFound() : Ok(flightReservation);
    }


    [HttpPost("Reserve")]
    public async Task<IActionResult> Add([FromRoute]int flightId,[FromRoute] int classId, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Received flightId: {flightId}, classId: {classId}\n\n\n\n\n\n\n\n\n\n");

        var ourReservation = await _flightReservationService.AddAsync(flightId, classId, cancellationToken);


        return ourReservation is null ? BadRequest("Failed to create reservation. Please check flight and class availability") 
            :CreatedAtAction(nameof(Get), new { id = ourReservation?.Id,flightId, classId }, ourReservation.Adapt<FlightReservationResponse>());

    }

}
