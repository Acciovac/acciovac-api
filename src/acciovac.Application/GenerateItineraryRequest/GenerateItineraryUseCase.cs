namespace acciovac.API.GenerateItineraryRequest
{
    public class GenerateItineraryUseCase
    {
        private readonly IAIItineraryService _ai;

        public GenerateItineraryUseCase(IAIItineraryService ai)
        {
            _ai = ai;
        }

        public async Task<ItineraryResponse> ExecuteAsync(GenerateItineraryRequest request)
        {
            var prompt = $"""
        Create a {request.Days}-day travel itinerary for {request.Destination}.
        Interests: {string.Join(", ", request.Interests)}.
        Return JSON with fields: destination, days, daysPlan[{{day,activities[]}}]
        """;

            var aiResultJson = await _ai.GenerateItineraryAsync(prompt);

            return JsonSerializer.Deserialize<ItineraryResponse>(aiResultJson)!;
        }
    }

}
