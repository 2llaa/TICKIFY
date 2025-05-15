using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tickfy.Contracts.Hotels;
using Tickfy.Services.Hotels;

namespace Tickfy.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HotelController(IHotelService hotelServic) : ControllerBase
{
    private readonly IHotelService _hotelServic = hotelServic;

    //[HttpGet]
    //public async Task<IActionResult>GetAll(CancellationToken cancellationToken)
    //{

    //    var result = await _hotelServic.GetAllAsync(cancellationToken);

    //    return result.IsSuccess ?
    //        Ok(result.Value) :
    //        NotFound(result.Error);
    //}

    //[HttpGet("search/by-rating")]
    //public async Task<IActionResult> GetHotelsByLocationAndRating( [FromQuery] int starRating, CancellationToken cancellationToken)
    //{
    //    var result = await _hotelServic.GetHotelsByStarRatingAsync( starRating, cancellationToken);
    //    return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    //}
    [HttpGet("filter")]
    public async Task<IActionResult> GetFilteredHotels([FromQuery] List<int>? ratings, CancellationToken cancellationToken)
    {
        var result = await _hotelServic.GetFilteredHotelsAsync(ratings, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

}
