using acciovac.API.Common;
using acciovac.Application.Behaviors.BudgetRanges.Commands.AddBudgetRange;
using acciovac.Application.Behaviors.BudgetRanges.Commands.UpdateBudgetRange;
using acciovac.Application.Behaviors.BudgetRanges.Queries.GetBudgetRanges;
using acciovac.Application.Behaviors.BudgetRanges.Queries.GetBudgetRangeById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace acciovac.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetRangesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BudgetRangesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] AddBudgetRangeRequest request)
        {
            var command = new AddBudgetRangeCommand(request.Type, request.FromBudget, request.ToBudget, request.IsActive);
            var result = await _mediator.Send(command);
            
            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.Failure(result.Error));
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Value },
                ApiResponse.Success(new { id = result.Value, message = "Budget range created successfully" }));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetBudgetRangesQuery());

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.Failure(result.Error));
            }

            var response = result.Value?.Select(r => new
            {
                id = r.Id,
                type = r.Type,
                fromBudget = r.FromBudget,
                toBudget = r.ToBudget,
                isActive = r.IsActive
            }) ?? Enumerable.Empty<object>();

            return Ok(ApiResponse.Success(response));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetBudgetRangeByIdQuery(id));
            
            if (!result.IsSuccess)
            {
                return NotFound(ApiResponse.Failure(result.Error));
            }

            var range = result.Value;

            return Ok(ApiResponse.Success(new
            {
                id = range!.Id,
                type = range.Type,
                fromBudget = range.FromBudget,
                toBudget = range.ToBudget,
                isActive = range.IsActive
            }));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBudgetRangeRequest request)
        {
            var command = new UpdateBudgetRangeCommand(id, request.Type, request.FromBudget, request.ToBudget, request.IsActive);
            var result = await _mediator.Send(command);
            
            if (!result.IsSuccess)
            {
                if (result.Error == "Budget range not found")
                {
                    return NotFound(ApiResponse.Failure(result.Error));
                }
                return BadRequest(ApiResponse.Failure(result.Error));
            }

            return Ok(ApiResponse.Success(new { id = result.Value, message = "Budget range updated successfully" }));
        }
    }

    public record AddBudgetRangeRequest(byte Type, decimal FromBudget, decimal ToBudget, bool IsActive = true);
    public record UpdateBudgetRangeRequest(byte Type, decimal FromBudget, decimal ToBudget, bool IsActive);
}
