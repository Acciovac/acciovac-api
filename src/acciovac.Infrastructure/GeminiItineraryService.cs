using acciovac.API.GenerateItineraryRequest;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace acciovac.Infrastructure
{
    public class GeminiItineraryService : IAIItineraryService
    {
        private readonly HttpClient _http;
        private readonly string _apiKey;

        public GeminiItineraryService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _apiKey = config["Gemini:ApiKey"]!;
        }

        public async Task<string> GenerateItineraryAsync(string prompt)
        {
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={_apiKey}";

            var body = new
            {
                contents = new[]
                {
                new
                {
                    role = "user",
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            }
            };

            var response = await _http.PostAsJsonAsync(url, body);
            var json = await response.Content.ReadAsStringAsync();

            return ExtractGeminiText(json); // You parse text from Gemini response
        }
    }

}
