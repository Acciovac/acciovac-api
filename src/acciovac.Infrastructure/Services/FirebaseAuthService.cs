using acciovac.Application.Abstractions;
using FirebaseAdmin.Auth;

namespace acciovac.Infrastructure.Services
{
    public class FirebaseAuthService : IFirebaseAuthService
    {
        private readonly FirebaseAuth _firebaseAuth;

        public FirebaseAuthService(FirebaseAuth firebaseAuth)
        {
            _firebaseAuth = firebaseAuth;
        }

        public async Task<string> VerifyTokenAsync(string idToken)
        {
            var decodedToken = await _firebaseAuth.VerifyIdTokenAsync(idToken);
            return decodedToken.Uid;
        }
    }
}
