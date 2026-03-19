using System.Net.Http.Json;
using acciovac.Application.Abstractions;
using Microsoft.Extensions.Configuration;

namespace acciovac.Infrastructure.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _http;
        private readonly string _apiKey;

        public WeatherService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _apiKey = config["Weather:ApiKey"] ?? string.Empty;
        }

        public async Task<string> GetWeatherAsync(string location)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={Uri.EscapeDataString(location)}&appid={_apiKey}&units=metric";

            var res = await _http.GetFromJsonAsync<WeatherResponse>(url);
            if (res?.Weather is null || res.Weather.Count == 0 || res.Main is null)
            {
                return string.Empty;
            }

            return $"{res.Weather[0].Main}, {res.Main.Temp}°C";
        }

        private sealed class WeatherResponse
        {
            public List<WeatherInfo> Weather { get; set; } = new();
            public MainInfo? Main { get; set; }
        }

        private sealed class WeatherInfo
        {
            public string Main { get; set; } = string.Empty;
        }

        private sealed class MainInfo
        {
            public double Temp { get; set; }
        }
    }
}
