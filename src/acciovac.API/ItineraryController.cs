using acciovac.API.GenerateItineraryRequest;
using Microsoft.AspNetCore.Mvc;

namespace acciovac.API
{
    [ApiController]
    [Route("api/itinerary")]
    public class ItineraryController : ControllerBase
    {
        private readonly GenerateItineraryUseCase _useCase;

        public ItineraryController(GenerateItineraryUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> Generate([FromBody] GenerateItineraryRequest request)
        {
            var result = await _useCase.ExecuteAsync(request);
            return Ok(result);
        }
    }

}
