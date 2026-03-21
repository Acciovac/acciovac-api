using System.Threading.Tasks;

namespace acciovac.Application.Abstractions
{
    public interface IFirebaseAuthService
    {
        Task<string> VerifyTokenAsync(string idToken);
    }
}
