using System.Net.Http.Json;
using System.Text.Json.Serialization;
using acciovac.Application.Abstractions;
using Microsoft.Extensions.Configuration;

namespace acciovac.Infrastructure.Services
{
    public class UnsplashImageService : IUnsplashImageService
    {
        private readonly HttpClient _http;
        private readonly string _accessKey;
        private readonly Dictionary<string, string> _cache = new(StringComparer.OrdinalIgnoreCase);

        public UnsplashImageService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _accessKey = config["Unsplash:AccessKey"] ?? string.Empty;
        }

        public async Task<string> GetImageUrlAsync(string location, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                return string.Empty;
            }

            if (_cache.TryGetValue(location, out var cachedUrl))
            {
                return cachedUrl;
            }

            if (string.IsNullOrWhiteSpace(_accessKey))
            {
                return string.Empty;
            }

            foreach (var query in BuildQueries(location))
            {
                var imageUrl = await SearchFirstImageUrlAsync(query, cancellationToken);
                if (!string.IsNullOrWhiteSpace(imageUrl))
                {
                    _cache[location] = imageUrl;
                    return imageUrl;
                }
            }

            _cache[location] = string.Empty;
            return string.Empty;
        }

        private async Task<string> SearchFirstImageUrlAsync(string query, CancellationToken cancellationToken)
        {
            var url = "https://api.unsplash.com/search/photos" +
                      $"?query={Uri.EscapeDataString(query)}" +
                      "&per_page=1" +
                      "&page=1" +
                      "&orientation=landscape" +
                      "&content_filter=high" +
                      "&order_by=relevant";

            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.TryAddWithoutValidation("Accept-Version", "v1");
            request.Headers.TryAddWithoutValidation("Authorization", $"Client-ID {_accessKey}");

            var response = await _http.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return string.Empty;
            }

            var payload = await response.Content.ReadFromJsonAsync<UnsplashSearchResponse>(cancellationToken: cancellationToken);
            return payload?.Results?.FirstOrDefault()?.Urls?.Regular ?? string.Empty;
        }

        private static IEnumerable<string> BuildQueries(string location)
        {
            var normalized = location.Trim();
            yield return normalized;

            var parts = normalized
                .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > 1)
            {
                yield return parts[^1];
                yield return parts[0];
            }
        }

        private sealed class UnsplashSearchResponse
        {
            [JsonPropertyName("results")]
            public List<UnsplashPhoto> Results { get; set; } = new();
        }

        private sealed class UnsplashPhoto
        {
            [JsonPropertyName("urls")]
            public UnsplashPhotoUrls? Urls { get; set; }
        }

        private sealed class UnsplashPhotoUrls
        {
            [JsonPropertyName("regular")]
            public string Regular { get; set; } = string.Empty;
        }
    }
}
