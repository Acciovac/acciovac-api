using acciovac.Application.Abstractions;
using acciovac.Domain.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace acciovac.Application.Behaviors.Tours.GenerateItinerary
{
    public sealed class GenerateItineraryCommandHandler : IRequestHandler<GenerateItineraryCommand, AiItineraryResponseDto>
    {
        private readonly IAppDbContext _context;
        private readonly IItineraryPlanner _planner;
        private readonly IGoogleMapsService _googleMaps;

        public GenerateItineraryCommandHandler(
            IAppDbContext context,
            IItineraryPlanner planner,
            IGoogleMapsService googleMaps)
        {
            _context = context;
            _planner = planner;
            _googleMaps = googleMaps;
        }

        public async Task<AiItineraryResponseDto> Handle(GenerateItineraryCommand request, CancellationToken cancellationToken)
        {
            var rules = await _context.AiRules
                .Where(r => r.IsActive)
                .OrderBy(r => r.Priority)
                .Select(r => r.RuleText)
                .ToListAsync(cancellationToken);

            var systemRules = string.Join("\n", rules);
            var locationsText = string.Join(" → ", request.Locations);

            var userPrompt = $@"{request.UserPrompt}

Additional constraints:
- Planned route: {locationsText}
- Return strictly JSON with root: itineraryPlan
- Each activity must include visitLocation and title must include visitLocation
- Do not return alternate formats.";

            var plan = await _planner.GeneratePlanAsync(systemRules, userPrompt, cancellationToken);

            if (plan?.ItineraryPlan?.DailySchedule is null || plan.ItineraryPlan.DailySchedule.Count == 0)
            {
                throw new InvalidOperationException("AI returned empty or invalid itinerary plan");
            }

            foreach (var day in plan.ItineraryPlan.DailySchedule)
            {
                foreach (var activity in day.Activities)
                {
                    var geocodeTarget = ResolveLocation(activity, day.Location);
                    var (lat, lng) = await _googleMaps.GetLatLngAsync(geocodeTarget);
                    activity.Coordinates.Latitude = lat;
                    activity.Coordinates.Longitude = lng;
                }
            }

            return plan;
        }

        private static string ResolveLocation(AiActivityDto activity, string fallbackLocation)
        {
            if (!string.IsNullOrWhiteSpace(activity.VisitLocation))
            {
                return activity.VisitLocation;
            }

            if (!string.IsNullOrWhiteSpace(activity.Title))
            {
                return activity.Title;
            }

            return fallbackLocation;
        }
    }
}
