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

        public async Task<AiItineraryResponseDto> GeneratePlanAsync(string systemRules, string userPrompt, CancellationToken cancellationToken)
        {
            var client = new Client(apiKey: _options.ApiKey);

            var systemContent = new Content
            {
                Parts = [new Part { Text = systemRules }]
            };

            var schemaPrompt = @"
Return ONLY valid JSON with this exact shape:
{
  ""itineraryPlan"": {
    ""tripOverview"": {
      ""traveler"": ""string"",
      ""budget"": 0,
      ""theme"": ""string"",
      ""locations"": [""string""]
    },
    ""dailySchedule"": [
      {
        ""date"": ""yyyy-MM-dd"",
        ""location"": ""string"",
        ""imageLink"": ""https://..."",
        ""activities"": [
          {
            ""startTime"": ""HH:mm:ss"",
            ""endTime"": ""HH:mm:ss"",
            ""title"": ""string"",
            ""description"": ""string""
          }
        ]
      }
    ],
    ""budgetBreakdown"": {
      ""currency"": ""string"",
      ""totalEstimated"": 0,
      ""categories"": {
        ""transport"": 0,
        ""accommodation"": 0,
        ""activities"": 0,
        ""foodAndDrinks"": 0,
        ""miscellaneous"": 0
      }
    }
  }
}
Do not add markdown, explanations, or alternative formats.";

            var userContent = new Content
            {
                Parts = [
                    new Part { Text = userPrompt },
                    new Part { Text = schemaPrompt }
                ]
            };

            var config = new GenerateContentConfig
            {
                SystemInstruction = systemContent,
                ResponseMimeType = "application/json",
                Temperature = 0.1f
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
                var result = JsonSerializer.Deserialize<AiItineraryResponseDto>(trimmed, serializerOptions);

                if (result?.ItineraryPlan?.DailySchedule is null || result.ItineraryPlan.DailySchedule.Count == 0)
                {
                    throw new Exception($"Gemini response missing required itineraryPlan.dailySchedule. Raw: {SafeSnippet(trimmed)}");
                }

                return result;
            }
            catch (JsonException ex)
            {
                throw new Exception($"Failed to parse Gemini response: {ex.Message}. Raw: {SafeSnippet(trimmed)}", ex);
            }
        }

        private static string SafeSnippet(string text)
        {
            const int max = 500;
            return text.Length <= max ? text : text.Substring(0, max) + "...";
        }
    }
}
