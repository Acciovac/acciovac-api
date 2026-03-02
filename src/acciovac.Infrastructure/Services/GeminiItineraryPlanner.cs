using acciovac.Application.Abstractions;
using acciovac.Domain.DTOs;
using acciovac.Infrastructure.Configuration;
using Google.GenAI;
using Google.GenAI.Types;
using System.Text.Json;

namespace acciovac.Infrastructure.Services
{
    public class GeminiItineraryPlanner : IItineraryPlanner
    {
        private readonly AiOptions _options;

        public GeminiItineraryPlanner(AiOptions options)
        {
            _options = options;
        }
        public async Task<List<AiDayDto>> GeneratePlanAsync(string systemRules, string userPrompt, CancellationToken cancellationToken)
        {
            var client = new Client(apiKey: _options.ApiKey);

            var systemContent = new Content
            {
                Parts = [new Part { Text = systemRules }]
            };

            var userContent = new Content
            {
                Parts = [new Part { Text = userPrompt }]
            };

            var config = new GenerateContentConfig
            {
                SystemInstruction = systemContent,
                ResponseMimeType = "application/json",
                Temperature = 0.7f
            };

            var response = await client.Models.GenerateContentAsync(
                model: _options.ModelId,
                contents: userContent,
                config: config
            );

            var generatedText = response.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text;

            if (string.IsNullOrEmpty(generatedText))
            {
                throw new Exception("Gemini returned an empty response.");
            }

            var serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<List<AiDayDto>>(generatedText, serializerOptions)
                   ?? [];
        }
    }
}
