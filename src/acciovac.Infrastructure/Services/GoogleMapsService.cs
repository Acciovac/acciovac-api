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
        private readonly Dictionary<string, (double lat, double lng)> _geocodeCache = new(StringComparer.OrdinalIgnoreCase);

        public GoogleMapsService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _apiKey = config["GoogleMaps:ApiKey"] ?? string.Empty;
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

        public async Task<(double lat, double lng)> GetLatLngAsync(string place)
        {
            if (string.IsNullOrWhiteSpace(place))
            {
                return (0, 0);
            }

            var cleanPlace = place.Split('/').Last().Trim();

            if (_geocodeCache.TryGetValue(cleanPlace, out var cached))
            {
                return cached;
            }

            var url = $"https://maps.googleapis.com/maps/api/geocode/json" +
                      $"?address={Uri.EscapeDataString(cleanPlace)}" +
                      $"&key={_apiKey}";

            var res = await _http.GetFromJsonAsync<GeocodeResponse>(url);
            var location = res?.Results?.FirstOrDefault()?.Geometry?.Location;

            if (location is null)
            {
                return (0, 0);
            }

            var result = (location.Lat, location.Lng);
            _geocodeCache[cleanPlace] = result;

            return result;
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

        private sealed class GeocodeResponse
        {
            public List<GeocodeResult> Results { get; set; } = new();
        }

        private sealed class GeocodeResult
        {
            public Geometry Geometry { get; set; } = new();
        }

        private sealed class Geometry
        {
            public GeoLocation Location { get; set; } = new();
        }

        private sealed class GeoLocation
        {
            public double Lat { get; set; }
            public double Lng { get; set; }
        }
    }
}
