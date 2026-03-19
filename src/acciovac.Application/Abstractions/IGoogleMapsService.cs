using System.Threading.Tasks;

namespace acciovac.Application.Abstractions
{
    public interface IGoogleMapsService
    {
        Task<double> GetTravelTimeHoursAsync(string from, string to);
    }
}
