using Microsoft.AspNetCore.Mvc;
using Tickfy.Services.Driver;

namespace Tickfy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet("by-star")]
        public async Task<IActionResult> GetDriversByStarRatingDescending(CancellationToken cancellationToken)
        {
            var result = await _driverService.GetDriversByStarRatingDescendingAsync(cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }


        [HttpGet("by-price")]
        public async Task<IActionResult> GetDriversByPriceAscending(CancellationToken cancellationToken)
        {
            var result = await _driverService.GetDriversByPriceAscendingAsync(cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }
    }

}
