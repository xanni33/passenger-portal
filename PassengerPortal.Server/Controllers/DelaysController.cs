using MediatR;
using Microsoft.AspNetCore.Mvc;
using PassengerPortal.Server.Builders;

namespace PassengerPortal.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DelaysController : ControllerBase
{
    private readonly IMediator _mediator;

    public DelaysController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDelay(CreateDelayCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetDelays()
    {
        // Pobierz listę opóźnień za pomocą Mediatora
        var delays = await _mediator.Send(new GetDelaysQuery());
        return Ok(delays);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDelay(int id)
    {
        var result = await _mediator.Send(new DeleteDelayCommand(id));
        if (!result)
            return NotFound();

        return NoContent();
    }
}
