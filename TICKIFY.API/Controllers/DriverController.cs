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

    //Get a driver by Id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDriverById(int id, CancellationToken cancellationToken)
    {
        var result = await _driverServices.GetDriverByIdAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errorr);
    }

    //Create a new driver (for Admin)
    [HttpPost]
    public async Task<IActionResult> CreateDriver([FromBody] DriverReq driverReq, CancellationToken cancellationToken)
    {
        if (driverReq is null)
            return BadRequest("Driver data is required.");

        var result = await _driverServices.CreateDriverAsync(driverReq, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetDriverById), new { id = result.Value.DriverId }, result.Value)
            : Problem(result.Errorr.Message);
    }

    //Update an existing driver (for Admin)
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDriver(int id, [FromBody] DriverReq driverReq, CancellationToken cancellationToken)
    {
        var result = await _driverServices.UpdateDriverAsync(id, driverReq, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Errorr);
    }

    //Delete a driver  (for Admin)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDriver(int id, CancellationToken cancellationToken)
    {
        var result = await _driverServices.GetDriverByIdAsync(id, cancellationToken);
        if (!result.IsSuccess) return NotFound(result.Errorr);

        await _driverServices.DeleteDriverAsync(id, cancellationToken);
        return NoContent();
    }
}

