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

        public GenerateItineraryCommandHandler(
            IAppDbContext context,
            IItineraryPlanner planner)
        {
            _context = context;
            _planner = planner;
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

            var userPrompt = $@"Plan a realistic trip.
                                Locations: {locationsText}
                                Rules: {systemRules}
                                Return strictly JSON with this root: itineraryPlan.
                                Do not return alternate formats.";

            var plan = await _planner.GeneratePlanAsync(systemRules, userPrompt, cancellationToken);

            if (plan?.ItineraryPlan?.DailySchedule is null || plan.ItineraryPlan.DailySchedule.Count == 0)
            {
                throw new InvalidOperationException("AI returned empty or invalid itinerary plan");
            }

            return plan;
        }
    }
}
