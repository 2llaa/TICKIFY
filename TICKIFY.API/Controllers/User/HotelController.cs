using Microsoft.AspNetCore.Mvc;
using TICKIFY.API.Contracts.Requests;
using TICKIFY.API.Services.Abstracts;
using TICKIFY.API.Contracts.hotels;
using TICKIFY.API.Contracts.Drivers;
using TICKIFY.API.Contracts.Rooms;
using Microsoft.AspNetCore.Authorization;

namespace TICKIFY.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "User")]

public class HotelController : ControllerBase
{
    private readonly IHotelServices _hotelServices;

    public HotelController(IHotelServices hotelServices)
    {
        _hotelServices = hotelServices;
    }
    //Get all hotels
    [HttpGet]
    public async Task<IActionResult> GetAllHotels(CancellationToken cancellationToken)
    {
        var result = await _hotelServices.GetHotelsByLocationAsync(string.Empty, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : Problem(result.Errorr.Message);
    }

    // Get hotel by ID
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetHotelById(int id, CancellationToken cancellationToken)
    {
        var result = await _hotelServices.GetHotelByIdAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errorr);
    }

    // Get hotel by name
    [HttpGet("search/by-name")]
    public async Task<IActionResult> GetHotelsByName([FromQuery] string name, CancellationToken cancellationToken)
    {
        string normalizedName = _hotelServices.NormalizeHotelName(name); 

        var result = await _hotelServices.GetHotelsByNameAsync(normalizedName, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : Problem(result.Errorr.Message);
    }

    // Get hotel by location and star rating
    [HttpGet("search/by-location-rating")]
    public async Task<IActionResult> GetHotelsByLocationAndRating([FromQuery] string location, [FromQuery] int starRating, CancellationToken cancellationToken)
    {
        var result = await _hotelServices.GetHotelsByLocationAndStarRatingAsync(location, starRating, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : Problem(result.Errorr.Message);
    }


    // Get hotel drivers
    [HttpGet("{id:int}/drivers")]
    public async Task<IActionResult> GetHotelDrivers(int id, CancellationToken cancellationToken)
    {
        var result = await _hotelServices.GetHotelDriversAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errorr);
    }

    // Get hotel rooms
    [HttpGet("{id:int}/rooms")]
    public async Task<IActionResult> GetHotelRooms(int id, CancellationToken cancellationToken)
    {
        var result = await _hotelServices.GetHotelRoomsAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errorr);
    }

}
