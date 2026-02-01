using MediatR;
using AccioVac.Domain.Entities;
using AccioVac.Application.Common.Interfaces;

namespace AccioVac.Application.Trips.Commands;

// 1. The Request DTO
public record CreateTripCommand(string Destination, string Interests, int Days) : IRequest<Guid>;

// 2. The Handler
public class CreateTripCommandHandler(
    IAppDbContext dbContext,
    ICurrentUserService currentUser,
    IAiService aiService) : IRequestHandler<CreateTripCommand, Guid>
{
    public async Task<Guid> Handle(CreateTripCommand request, CancellationToken ct)
    {
        // 1. Generate Itinerary (This calls Gemini/OpenAI)
        // We do this BEFORE saving to ensure we have content.
        var jsonItinerary = await aiService.GenerateItineraryAsync(request.Destination, request.Interests, request.Days);

        // 2. Create Entity
        var trip = new Trip
        {
            UserId = currentUser.UserId, // Uses the "Dev User" automatically
            Destination = request.Destination,
            UserInterests = request.Interests,
            DurationDays = request.Days,
            AiItineraryJson = jsonItinerary,
            IsGenerated = true
        };

        // 3. Save to Postgres
        dbContext.Trips.Add(trip);
        await dbContext.SaveChangesAsync(ct);

        return trip.Id;
    }
}