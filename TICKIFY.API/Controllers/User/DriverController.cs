using Microsoft.AspNetCore.Mvc;
using TICKIFY.API.Contracts.Requests;
using TICKIFY.API.Services.Abstracts;
using TICKIFY.API.Contracts.Drivers;
using Microsoft.AspNetCore.Authorization;

namespace TICKIFY.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "User")]

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

}

