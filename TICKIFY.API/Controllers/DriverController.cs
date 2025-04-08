using Microsoft.AspNetCore.Mvc;
using TICKIFY.API.Contracts.Requests;
using TICKIFY.API.Services.Abstracts;
using TICKIFY.API.Contracts.Drivers;

namespace TICKIFY.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriverController : ControllerBase
{
    private readonly IDriverServices _driverServices;

    public DriverController(IDriverServices driverServices)
    {
        _driverServices = driverServices;
    }

    /// <summary>Get all drivers</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DriverRes>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllDrivers(CancellationToken cancellationToken)
    {
        var result = await _driverServices.GetAllDriversAsync(cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : Problem(result.Errorr.Message);
    }

    /// <summary>Get a driver by ID</summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DriverRes), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDriverById(int id, CancellationToken cancellationToken)
    {
        var result = await _driverServices.GetDriverByIdAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errorr);
    }

    /// <summary>Get all drivers for a specific hotel</summary>
    [HttpGet("hotel/{hotelId}")]
    [ProducesResponseType(typeof(IEnumerable<DriverRes>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDriversByHotelId(int hotelId)
    {
        var result = await _driverServices.GetDriversByHotelIdAsync(hotelId);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errorr);
    }

    /// <summary>Create a new driver</summary>
    [HttpPost]
    [ProducesResponseType(typeof(DriverRes), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateDriver([FromBody] DriverReq driverReq, CancellationToken cancellationToken)
    {
        if (driverReq is null)
            return BadRequest("Driver data is required.");

        var result = await _driverServices.CreateDriverAsync(driverReq, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetDriverById), new { id = result.Value.DriverId }, result.Value)
            : Problem(result.Errorr.Message);
    }

    /// <summary>Update an existing driver</summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(DriverRes), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateDriver(int id, [FromBody] DriverReq driverReq, CancellationToken cancellationToken)
    {
        var result = await _driverServices.UpdateDriverAsync(id, driverReq, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Errorr);
    }

    /// <summary>Delete a driver</summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDriver(int id, CancellationToken cancellationToken)
    {
        var result = await _driverServices.GetDriverByIdAsync(id, cancellationToken);
        if (!result.IsSuccess) return NotFound(result.Errorr);

        await _driverServices.DeleteDriverAsync(id, cancellationToken);
        return NoContent();
    }
}
//get drivers by hotel id
//create driver
//update driver
//delete driver
//get driver by id
