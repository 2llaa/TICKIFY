using Microsoft.AspNetCore.Mvc;
using TICKIFY.API.Contracts.Requests;
using TICKIFY.API.Services.Abstracts;
using TICKIFY.API.Contracts.hotels;
using TICKIFY.API.Contracts.Drivers;
using TICKIFY.API.Contracts.Rooms;
using Microsoft.AspNetCore.Authorization;

namespace TICKIFY.API.Controllers.Admin;

[ApiController]
[Route("api/Admin/[controller]")]
[Authorize(Roles = "Admin")]

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



    // Create hotel (for Admin)
    [HttpPost]
    public async Task<IActionResult> CreateHotel([FromBody] HotelReq hotelReq, CancellationToken cancellationToken)
    {
        if (hotelReq is null)
            return BadRequest("Hotel data is required.");

        var result = await _hotelServices.CreateHotelAsync(hotelReq, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetHotelById), new { id = result.Value.HotelId }, result.Value)
            : Problem(result.Errorr.Message);
    }

    // Update hotel (for Admin)
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateHotel(int id, [FromBody] HotelReq hotelReq, CancellationToken cancellationToken)
    {
        var result = await _hotelServices.UpdateHotelAsync(id, hotelReq, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Errorr);
    }

    // Delete hotel (for Admin)
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteHotel(int id, CancellationToken cancellationToken)
    {
        var result = await _hotelServices.GetHotelByIdAsync(id, cancellationToken);
        if (!result.IsSuccess) return NotFound(result.Errorr);

        await _hotelServices.DeleteHotelAsync(id, cancellationToken);
        return NoContent();
    }
    // Soft delete hotel (for Admin)
    [HttpDelete("soft/{id:int}")]
    public async Task<IActionResult> SoftDeleteHotel(int id, CancellationToken cancellationToken)
    {
        var result = await _hotelServices.SoftDeleteHotelAsync(id, cancellationToken);
        return result.IsSuccess ? NoContent() : NotFound(result.Errorr.Message);
    }
}
