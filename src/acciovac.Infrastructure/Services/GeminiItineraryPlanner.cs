using acciovac.Application.Abstractions;
using acciovac.Domain.DTOs;
using acciovac.Infrastructure.Configuration;
using Google.GenAI;
using Google.GenAI.Types;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace acciovac.Infrastructure.Services
{
    public class GeminiItineraryPlanner : IItineraryPlanner
    {
        private readonly AiOptions _options;

        public GeminiItineraryPlanner(IOptions<AiOptions> options) => _options = options.Value;
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
                Temperature = 0.3f
            };

            var response = await client.Models.GenerateContentAsync(
                model: _options.ModelId,
                contents: userContent,
                config: config,
                cancellationToken: cancellationToken
            );

            var generatedText = response.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text;

            if (string.IsNullOrWhiteSpace(generatedText))
            {
                throw new Exception("Gemini returned an empty response.");
            }

            var trimmed = generatedText.Trim();
            var serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            try
            {
                if (trimmed.StartsWith("["))
                {
                    return JsonSerializer.Deserialize<List<AiDayDto>>(trimmed, serializerOptions) ?? [];
                }

                var single = JsonSerializer.Deserialize<AiDayDto>(trimmed, serializerOptions);
                if (single is not null)
                {
                    return new List<AiDayDto> { single };
                }
            }
            catch (JsonException ex)
            {
                throw new Exception($"Failed to parse Gemini response: {ex.Message}. Raw: {SafeSnippet(trimmed)}", ex);
            }

            throw new Exception($"Gemini response was not valid JSON array. Raw: {SafeSnippet(trimmed)}");
        }

        private static string SafeSnippet(string text)
        {
            const int max = 500;
            return text.Length <= max ? text : text.Substring(0, max) + "...";
        }
    }
}
