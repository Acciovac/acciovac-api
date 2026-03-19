using System.Threading.Tasks;

namespace acciovac.Application.Abstractions
{
    public interface IWeatherService
    {
        Task<string> GetWeatherAsync(string location);
    }
}
