using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tickfy.Contracts.Classes;
using Tickfy.Enums;
using Tickfy.Services.Classes;

namespace Tickfy.Controllers;
[Route("api/Flights/{flightId}/[controller]")]
[ApiController]
public class ClassesController(IClassService classService) : ControllerBase
{
    private readonly IClassService _classService = classService;



    [HttpGet("Types")]
    public IActionResult GetClassTypes()
    {
        var classTypes = Enum.GetValues(typeof(ClassType))
                             .Cast<ClassType>()
                             .Select(ct => ct.ToString())
                             .ToList();

        return Ok(classTypes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int flightId, int id, CancellationToken cancellationToken = default!)
    {
        var result = await _classService.GetAsync(flightId, id, cancellationToken);

        return result.IsSuccess ?
            Ok(result.Value) :
            NotFound(result.Error);
    }

    [HttpPost()]
    public async Task<IActionResult> Add([FromRoute]int flightId,[FromBody] CreateClassRequest classRequest, CancellationToken cancellationToken = default!)
    {
       var result = await _classService.AddAsync(flightId, classRequest, cancellationToken);

        return result.IsSuccess ?
            CreatedAtAction(nameof(Get), new { id = result.Value.Id, flightId}, result.Value) :
            BadRequest(result.Error);
    }


}
