using Crm.Application.Features.Deals.Commands;
using Crm.Application.Features.Deals.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DealsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DealsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var deals = await _mediator.Send(new GetAllDealsQuery());
        return Ok(deals);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var deal = await _mediator.Send(new GetDealByIdQuery(id));
        return deal != null ? Ok(deal) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDealCommand command)
    {
        var dealId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = dealId }, dealId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDealCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID mismatch.");
        }
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteDealCommand(id));
        return NoContent();
    }

    [HttpGet("stages")]
    [AllowAnonymous] // Anyone can see the list of possible stages
    public IActionResult GetStageOptions()
    {
        var stages = Enum.GetNames(typeof(Stage));
        return Ok(stages);
    }
}