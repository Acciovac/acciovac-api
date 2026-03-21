using System.Threading.Tasks;

namespace acciovac.Application.Abstractions
{
    public interface IGoogleMapsService
    {
        Task<double> GetTravelTimeHoursAsync(string from, string to);
        Task<(double lat, double lng)> GetLatLngAsync(string place);
        Task<string?> GetPhotoUrlAsync(string placeName, CancellationToken cancellationToken = default);
    }
}
