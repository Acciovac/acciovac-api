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
