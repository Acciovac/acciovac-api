using acciovac.API.Common;
using acciovac.Application.Abstractions;
using acciovac.Application.Behaviors.AiRules.Commands.CreateAiRule;
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
        private readonly IAiRuleRepository _aiRuleRepository;

        public AiRulesController(IMediator mediator, IAiRuleRepository aiRuleRepository)
        {
            _mediator = mediator;
            _aiRuleRepository = aiRuleRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActiveRules()
        {
            var rules = await _aiRuleRepository.GetActiveRulesAsync();

            var response = rules.Select(r => new
            {
                id = r.Id,
                code = r.Code,
                ruleText = r.RuleText,
                priority = r.Priority,
                isActive = r.IsActive,
                createdAt = r.CreatedAt
            });

            return Ok(ApiResponse.Success(response));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateAiRuleRequest request)
        {
            var command = new CreateAiRuleCommand(request.Code, request.RuleText, request.Priority, request.CreatedBy);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.Failure(result.Error));
            }

            return CreatedAtAction(
                nameof(GetActiveRules),
                new { id = result.Value },
                ApiResponse.Success(new { id = result.Value, message = "AI rule created successfully" }));
        }
    }

    public record CreateAiRuleRequest(string Code, string RuleText, int Priority, string CreatedBy);
}
