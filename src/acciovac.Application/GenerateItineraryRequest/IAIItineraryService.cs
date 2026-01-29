namespace acciovac.API.GenerateItineraryRequest
{
    public interface IAIItineraryService
    {
        Task<string> GenerateItineraryAsync(string prompt);
    }

}
