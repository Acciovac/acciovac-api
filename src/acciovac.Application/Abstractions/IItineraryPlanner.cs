using acciovac.Domain.DTOs;

namespace acciovac.Application.Abstractions
{
    public interface IItineraryPlanner
    {
        Task<List<AiDayDto>> GeneratePlanAsync(
            string systemRules,
            string userPrompt,
            CancellationToken cancellationToken);
    }
}
