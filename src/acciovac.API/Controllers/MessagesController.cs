using acciovac.Application.Behaviors.Messages.Commands.DeleteMessage;
using acciovac.Application.Behaviors.Messages.Commands.MarkMessageAsRead;
using acciovac.Application.Behaviors.Messages.Commands.MarkMessageAsResolved;
using acciovac.Application.Behaviors.Messages.Queries.GetUserMessages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace acciovac.API.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserMessages([FromQuery] Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest(new { error = "Query parameter 'userId' is required." });
            }

            var result = await _mediator.Send(new GetUserMessagesQuery(userId));

            return Ok(result.Value);
        }

        [HttpPut("{id:guid}/read")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MarkAsRead(Guid id, [FromQuery] string? modifiedBy)
        {
            var result = await _mediator.Send(new MarkMessageAsReadCommand(id, modifiedBy));

            if (!result.IsSuccess)
            {
                return NotFound(new { error = result.Error });
            }

            return Ok(new { id = result.Value, message = "Message marked as read" });
        }

        [HttpPut("{id:guid}/resolve")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MarkAsResolved(Guid id, [FromQuery] string? modifiedBy)
        {
            var result = await _mediator.Send(new MarkMessageAsResolvedCommand(id, modifiedBy));

            if (!result.IsSuccess)
            {
                return NotFound(new { error = result.Error });
            }

            return Ok(new { id = result.Value, message = "Message marked as resolved" });
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteMessageCommand(id));

            if (!result.IsSuccess)
            {
                return NotFound(new { error = result.Error });
            }

            return Ok(new { message = "Message deleted successfully" });
        }
    }
}
