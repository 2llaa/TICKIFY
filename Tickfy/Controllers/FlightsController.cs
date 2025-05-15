using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Tickfy.Abstractions;
using Tickfy.Contracts.Classes;
using Tickfy.Contracts.Flights;
using Tickfy.Entities;
using Tickfy.Services.Flights;

namespace Tickfy.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FlightsController(IFlightService flightService) : ControllerBase
{
    private readonly IFlightService _flightService = flightService;


    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default!)
    {
        var result = await _flightService.GetAllAsync(cancellationToken);


        return result.IsSuccess ?
            Ok(result.Value) :
            NotFound(result.Error);
    }

    [HttpGet( "{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default!)
    {
        var result = await _flightService.GetAsync(id, cancellationToken);

        return result.IsSuccess ?
            Ok(result.Value) :
            NotFound(result.Error);
     
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchFlight([FromQuery] SearchFlightRequest flightRequest, CancellationToken cancellationToken = default!)

    {

        var result = await _flightService.SearchFlightAsync(flightRequest, cancellationToken);

        return result.IsSuccess ?
            Ok(result.Value) :
            NotFound(result.Error);

    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateFlightRequest flightRequest, CancellationToken cancellationToken = default!)
    {
        var result = await _flightService.AddAsync(flightRequest, cancellationToken);

        return result.IsSuccess ?
            CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value) :
            BadRequest(result.Error);
    }

    [HttpPut("{id}/UpdateDate")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDateFlightRequest flightRequest,CancellationToken cancellationToken)
    {
        var result = await _flightService.UpdateDateAsync(id, flightRequest, cancellationToken);


        return result.IsSuccess ? NoContent() :
            NotFound(result.Error);
    }



}
