using acciovac.Domain.DTOs;

namespace acciovac.Application.Abstractions
{
    public interface IItineraryPlanner
    {
        Task<AiItineraryResponseDto> GeneratePlanAsync(
            string systemRules,
            string userPrompt,
            CancellationToken cancellationToken);
    }
}
