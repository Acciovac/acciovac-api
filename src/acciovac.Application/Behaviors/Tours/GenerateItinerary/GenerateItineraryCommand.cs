using acciovac.Domain.DTOs;
using MediatR;
using System.Collections.Generic;

namespace acciovac.Application.Behaviors.Tours.GenerateItinerary
{
    public sealed record GenerateItineraryCommand(
        string UserPrompt,
        List<string> Locations
    ) : IRequest<List<AiDayDto>>;
}
