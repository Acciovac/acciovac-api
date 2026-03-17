using acciovac.Application.Abstractions;
using acciovac.Application.Behaviors.LocalExpirences.Commands.AddLocalExpirences;
using acciovac.Application.Behaviors.LocalExpirences.Commands.DeleteLocalExpirences;
using acciovac.Application.Behaviors.LocalExpirences.Commands.UpdateLocalExpirences;
using acciovac.Application.Behaviors.LocalExpirences.Queries.GetAllLocalExpirences;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace acciovac.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocalExpirencesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocalExpirencesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllLocalExpirencesQuery());

            return Ok(result.Value?.Select(x => new
            {
                id = x.Id,
                locationName = x.LocationName,
                description = x.Description,
                createdAt = x.CreatedAt
            }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] AddLocalExpirencesRequest request)
        {
            var command = new AddLocalExpirencesCommand(request.LocationName, request.Description);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(new { error = result.Error });
            }

            return CreatedAtAction(nameof(GetAll), new { id = result.Value }, new { id = result.Value, message = "Local experience added successfully" });
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLocalExpirencesRequest request)
        {
            var command = new UpdateLocalExpirencesCommand(id, request.LocationName, request.Description);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return NotFound(new { error = result.Error });
            }

            return Ok(new { id = result.Value, message = "Local experience updated successfully" });
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteLocalExpirencesCommand(id));

            if (!result.IsSuccess)
            {
                return NotFound(new { error = result.Error });
            }

            return Ok(new { message = "Local experience deleted successfully" });
        }
    }

    public record AddLocalExpirencesRequest(string LocationName, string Description);
    public record UpdateLocalExpirencesRequest(string LocationName, string Description);
}
