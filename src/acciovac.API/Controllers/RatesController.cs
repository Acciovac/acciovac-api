using acciovac.Application.Abstractions;
using acciovac.Application.Behaviors.Rates.Commands.UpdateRate;
using acciovac.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace acciovac.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRateRepository _rateRepository;

        public RatesController(IMediator mediator, IRateRepository rateRepository)
        {
            _mediator = mediator;
            _rateRepository = rateRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActiveRate()
        {
            var rate = await _rateRepository.GetActiveRateAsync();
            
            if (rate is null)
            {
                return Ok(new { message = "No rates found" });
            }

            return Ok(new 
            {
                id = rate.Id,
                baseFare = rate.BaseFare,
                perKm = rate.PerKm,
                // nightMultiplier = rate.NightMultiplier,
                // peakMultiplier = rate.PeakMultiplier,
                createdAt = rate.CreatedAt
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRate([FromBody] CreateRateRequest request)
        {
            var rate = new Rate(
                request.BaseFare,
                request.PerKm,
                // request.NightMultiplier ?? 1.0m,
                // request.PeakMultiplier ?? 1.0m,
                "system"
            );

            await _rateRepository.AddAsync(rate);

            return CreatedAtAction(
                nameof(GetActiveRate),
                new { id = rate.Id },
                new { id = rate.Id, message = "Rate created successfully" });
        }

        

        
    }

    public record CreateRateRequest(
        decimal BaseFare,
        decimal PerKm
        // decimal? NightMultiplier,
        // decimal? PeakMultiplier
    );

    public record UpdateRateRequest(
        decimal BaseFare,
        decimal PerKm
        // decimal? NightMultiplier,
        // decimal? PeakMultiplier
    );
}
