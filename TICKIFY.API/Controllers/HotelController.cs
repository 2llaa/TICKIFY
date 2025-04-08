using Microsoft.AspNetCore.Mvc;
using TICKIFY.API.Contracts.Requests;
using TICKIFY.API.Services.Abstracts;
using TICKIFY.API.Contracts.hotels;
using TICKIFY.API.Contracts.Drivers;
using TICKIFY.API.Contracts.Rooms;

namespace TICKIFY.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelController : ControllerBase
{
    private readonly IHotelServices _hotelServices;

    public HotelController(IHotelServices hotelServices)
    {
        _hotelServices = hotelServices;
    }

    // Get all hotels
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<HotelRes>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllHotels(CancellationToken cancellationToken)
    {
        var result = await _hotelServices.GetAllHotelsAsync(cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : Problem(result.Errorr.Message);
    }

    // Get hotel by ID
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(HotelRes), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetHotelById(int id, CancellationToken cancellationToken)
    {
        var result = await _hotelServices.GetHotelByIdAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errorr);
    }

    // Get hotel drivers
    [HttpGet("{id:int}/drivers")]
    [ProducesResponseType(typeof(IEnumerable<DriverRes>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHotelDrivers(int id, CancellationToken cancellationToken)
    {
        var result = await _hotelServices.GetHotelDriversAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errorr);
    }

    // Get hotel by name
    [HttpGet("search/by-name")]
    [ProducesResponseType(typeof(IEnumerable<SearchHotelRes>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHotelsByName([FromQuery] string name, CancellationToken cancellationToken)
    {
        var result = await _hotelServices.GetHotelsByNameAsync(name, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : Problem(result.Errorr.Message);
    }

    // Get hotel rooms
    [HttpGet("{id:int}/rooms")]
    [ProducesResponseType(typeof(IEnumerable<RoomRes>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHotelRooms(int id, CancellationToken cancellationToken)
    {
        var result = await _hotelServices.GetHotelRoomsAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errorr);
    }

    // Get hotel by location
    [HttpGet("search/by-location")]
    [ProducesResponseType(typeof(IEnumerable<HotelRes>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHotelsByLocation([FromQuery] string location, CancellationToken cancellationToken)
    {
        var result = await _hotelServices.GetHotelsByLocationAsync(location, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : Problem(result.Errorr.Message);
    }

    // Get hotel by star rating
    [HttpGet("search/by-rating")]
    [ProducesResponseType(typeof(IEnumerable<HotelRes>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHotelsByRating([FromQuery] int stars, CancellationToken cancellationToken)
    {
        var result = await _hotelServices.GetHotelsByStarRatingAsync(stars, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : Problem(result.Errorr.Message);
    }

    // Create hotel
    [HttpPost]
    [ProducesResponseType(typeof(HotelRes), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateHotel([FromBody] HotelReq hotelReq, CancellationToken cancellationToken)
    {
        if (hotelReq is null)
            return BadRequest("Hotel data is required.");

        var result = await _hotelServices.CreateHotelAsync(hotelReq, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetHotelById), new { id = result.Value.HotelId }, result.Value)
            : Problem(result.Errorr.Message);
    }

    // Update hotel
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(HotelRes), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateHotel(int id, [FromBody] HotelReq hotelReq, CancellationToken cancellationToken)
    {
        var result = await _hotelServices.UpdateHotelAsync(id, hotelReq, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Errorr);
    }

    // Delete hotel
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteHotel(int id, CancellationToken cancellationToken)
    {
        var result = await _hotelServices.GetHotelByIdAsync(id, cancellationToken);
        if (!result.IsSuccess) return NotFound(result.Errorr);

        await _hotelServices.DeleteHotelAsync(id, cancellationToken);
        return NoContent();
    }
}
//check the driver list in get hotel by star