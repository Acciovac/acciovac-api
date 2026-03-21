using acciovac.API.Common;
using acciovac.Application.Abstractions;
using acciovac.Application.Behaviors.Tours.GenerateItinerary;
using acciovac.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace acciovac.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItinerariesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IFirebaseAuthService _firebaseAuthService;

        public ItinerariesController(IMediator mediator, IFirebaseAuthService firebaseAuthService)
        {
            _mediator = mediator;
            _firebaseAuthService = firebaseAuthService;
        }

        [HttpPost]
        [Route("generate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Generate([FromBody] GenerateItineraryRequest request, CancellationToken cancellationToken)
        {
            //var authHeader = Request.Headers.Authorization.FirstOrDefault();

            //if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            //{
            //    return Unauthorized(ApiResponse.Failure("Missing or invalid Authorization header"));
            //}

            //var token = authHeader["Bearer ".Length..].Trim();

            //if (string.IsNullOrWhiteSpace(token))
            //{
            //    return Unauthorized(ApiResponse.Failure("Firebase token is required"));
            //}

            //try
            //{
            //    await _firebaseAuthService.VerifyTokenAsync(token);
            //}
            //catch
            //{
            //    return Unauthorized(ApiResponse.Failure("Invalid Firebase token"));
            //}

            if (request is null)
            {
                return BadRequest(ApiResponse.Failure("Request is required"));
            }

            var locations = new List<string>
            {
                request.StartLocation?.Name ?? string.Empty,
                request.EndLocation?.Name ?? string.Empty
            };

            var freeTimes = request.DailyFreeTimes?.Select(kvp => $"{kvp.Key}: {kvp.Value?.Start} - {kvp.Value?.End}") ?? Enumerable.Empty<string>();

            var userPrompt = $@"Plan an itinerary with these constraints:
                                Date range: {request.DateRange?.Start} to {request.DateRange?.End}
                                Budget: {request.Budget}
                                Travel type: {request.TravelType}
                                Interests: {string.Join(", ", request.Interests ?? new List<string>())}
                                Start location: {request.StartLocation?.Name} ({request.StartLocation?.Type}, {request.StartLocation?.Latitude}, {request.StartLocation?.Longitude})
                                End location: {request.EndLocation?.Name} ({request.EndLocation?.Type}, {request.EndLocation?.Latitude}, {request.EndLocation?.Longitude})
                                Daily free times:\n{string.Join("\n", freeTimes)}
                                Generate a short, catchy title (max 4 words) based on destination, theme, and travel type.

                                Return ONLY JSON with root key itineraryPlan and no alternate response formats.";

            var command = new GenerateItineraryCommand(userPrompt, locations);
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(ApiResponse.Success(result));
        }
    }
}
