using acciovac.Application.Abstractions;
using acciovac.Domain.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace acciovac.Application.Behaviors.Tours.GenerateItinerary
{
    public sealed class GenerateItineraryCommandHandler : IRequestHandler<GenerateItineraryCommand, List<AiDayDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IItineraryPlanner _planner;
        private readonly IGoogleMapsService _maps;
        private readonly IWeatherService _weather;

        public GenerateItineraryCommandHandler(
            IAppDbContext context,
            IItineraryPlanner planner,
            IGoogleMapsService maps,
            IWeatherService weather)
        {
            _context = context;
            _planner = planner;
            _maps = maps;
            _weather = weather;
        }

        public async Task<List<AiDayDto>> Handle(GenerateItineraryCommand request, CancellationToken cancellationToken)
        {
            var rules = await _context.AiRules
                .Where(r => r.IsActive)
                .OrderBy(r => r.Priority)
                .Select(r => r.RuleText)
                .ToListAsync(cancellationToken);

            var systemRules = string.Join("\n", rules);
            var locationsText = string.Join(" → ", request.Locations);

            var userPrompt = $@"Plan a realistic trip.
                                Locations: {locationsText}
                                Rules: {systemRules}
                                Return strictly JSON format.
                                Include travel times and logical routing.";

            var plan = await _planner.GeneratePlanAsync(systemRules, userPrompt, cancellationToken);

            if (plan == null || plan.Count == 0)
            {
                throw new InvalidOperationException("AI returned empty plan");
            }

            foreach (var day in plan)
            {
                day.BookingLink = $"https://www.booking.com/searchresults.html?ss={Uri.EscapeDataString(day.BaseLocation)}";

                for (var i = 0; i < day.Activities.Count; i++)
                {
                    var activity = day.Activities[i];
                    activity.Weather = await _weather.GetWeatherAsync(activity.PlaceName);

                    if (i == 0)
                    {
                        activity.TravelTimeFromPrevious = "0 min";
                        continue;
                    }

                    var previous = day.Activities[i - 1];
                    var hours = await _maps.GetTravelTimeHoursAsync(previous.PlaceName, activity.PlaceName);
                    activity.TravelTimeFromPrevious = $"{Math.Round(hours * 60)} min";
                }
            }

            return plan;
        }
    }
}
