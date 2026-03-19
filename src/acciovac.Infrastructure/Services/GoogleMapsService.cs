using System.Linq;
using System.Net.Http.Json;
using acciovac.Application.Abstractions;
using Microsoft.Extensions.Configuration;

namespace acciovac.Infrastructure.Services
{
    public class GoogleMapsService : IGoogleMapsService
    {
        private readonly HttpClient _http;
        private readonly string _apiKey;

        public GoogleMapsService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _apiKey = config["GoogleMaps:AIzaSyAxAbjFJQ37WUdYd32oH7VZFZwsTRgEhEQ"] ?? string.Empty;
        }

        public async Task<double> GetTravelTimeHoursAsync(string from, string to)
        {
            var url = $"https://maps.googleapis.com/maps/api/distancematrix/json" +
                      $"?origins={Uri.EscapeDataString(from)}" +
                      $"&destinations={Uri.EscapeDataString(to)}" +
                      "&departure_time=now" +
                      $"&key={_apiKey}";

            var res = await _http.GetFromJsonAsync<DistanceMatrixResponse>(url);
            var seconds = res?.Rows?.FirstOrDefault()?.Elements?.FirstOrDefault()?.Duration?.Value;

            if (seconds is null)
            {
                return 0;
            }

            return seconds.Value / 3600d;
        }

        private sealed class DistanceMatrixResponse
        {
            public List<Row> Rows { get; set; } = new();
        }

        private sealed class Row
        {
            public List<Element> Elements { get; set; } = new();
        }

        private sealed class Element
        {
            public DurationData? Duration { get; set; }
        }

        private sealed class DurationData
        {
            public double Value { get; set; }
        }
    }
}
