using acciovac.API.Common;
using acciovac.Application.Behaviors.AiRules.Commands.CreateAiRule;
using acciovac.Application.Behaviors.AiRules.Commands.DeactivateAiRule;
using acciovac.Application.Behaviors.AiRules.Queries.GetActiveAiRules;
using acciovac.Application.Behaviors.AiRules.Queries.GetAiRuleById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace acciovac.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiRulesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AiRulesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActiveRules()
        {
            var result = await _mediator.Send(new GetActiveAiRulesQuery());

            var response = result.Value?.Select(r => new
            {
                id = r.Id,
                code = r.Code,
                ruleText = r.RuleText,
                priority = r.Priority,
                isActive = r.IsActive,
                createdAt = r.CreatedAt
            }) ?? Enumerable.Empty<object>();

            return Ok(ApiResponse.Success(response));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetAiRuleByIdQuery(id));

            if (!result.IsSuccess)
            {
                return NotFound(ApiResponse.Failure(result.Error));
            }

            var rule = result.Value;

            return Ok(ApiResponse.Success(new
            {
                id = rule!.Id,
                code = rule.Code,
                ruleText = rule.RuleText,
                priority = rule.Priority,
                isActive = rule.IsActive,
                createdAt = rule.CreatedAt
            }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] List<CreateAiRuleRequest> requests)
        {
            if (requests is null || requests.Count == 0)
            {
                return BadRequest(ApiResponse.Failure("Request list is empty"));
            }

            var createdIds = new List<Guid>();

            foreach (var request in requests)
            {
                var command = new CreateAiRuleCommand(request.Code, request.RuleText, request.Priority);
                var result = await _mediator.Send(command);

                if (!result.IsSuccess)
                {
                    return BadRequest(ApiResponse.Failure(result.Error));
                }

                createdIds.Add(result.Value);
            }

            return CreatedAtAction(
                nameof(GetActiveRules),
                new { count = createdIds.Count },
                ApiResponse.Success(new { ids = createdIds, message = "AI rules created successfully" }));
        }

        [HttpPatch("{id:guid}/deactivate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var result = await _mediator.Send(new DeactivateAiRuleCommand(id));

            if (!result.IsSuccess)
            {
                if (result.Error == "AI rule not found")
                {
                    return NotFound(ApiResponse.Failure(result.Error));
                }

                return BadRequest(ApiResponse.Failure(result.Error));
            }

            return Ok(ApiResponse.Success(new { id = result.Value, message = "AI rule deactivated successfully" }));
        }
    }

    public record CreateAiRuleRequest(string Code, string RuleText, int Priority);
}
