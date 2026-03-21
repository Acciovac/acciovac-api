using acciovac.Application.Abstractions;
using acciovac.Infrastructure.Persistence;
using acciovac.Infrastructure.Repositories;
using acciovac.Infrastructure.Services;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;

namespace acciovac.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRateRepository, RateRepository>();
            services.AddScoped<ILocalExpirences, LocalExpirencesRepository>();
            services.AddScoped<IAiRuleRepository, AiRuleRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();

            services.AddHttpClient<IGoogleMapsService, GoogleMapsService>();
            services.AddHttpClient<IWeatherService, WeatherService>();
            services.AddHttpClient<IUnsplashImageService, UnsplashImageService>();

            var firebaseApp = FirebaseApp.DefaultInstance;

            if (firebaseApp is null)
            {
                var configuredPath = config["Firebase:ServiceAccountPath"];

                var candidates = new[]
                {
                    configuredPath,
                    "Configuration/serviceAccountKey.json",
                    Path.Combine("src", "acciovac.API", "Configuration", "serviceAccountKey.json")
                }
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .SelectMany(p => new[]
                {
                    p!,
                    Path.IsPathRooted(p!) ? p! : Path.Combine(Directory.GetCurrentDirectory(), p!),
                    Path.IsPathRooted(p!) ? p! : Path.Combine(AppContext.BaseDirectory, p!)
                })
                .Distinct()
                .ToList();

                var resolvedPath = candidates.FirstOrDefault(File.Exists);

                if (resolvedPath is null)
                {
                    throw new FileNotFoundException(
                        "Firebase service account file not found. Set Firebase:ServiceAccountPath or place serviceAccountKey.json under Configuration/.");
                }

                firebaseApp = FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile(resolvedPath)
                });
            }

            services.AddSingleton(firebaseApp);
            services.AddSingleton(FirebaseAuth.GetAuth(firebaseApp));
            services.AddScoped<IFirebaseAuthService, FirebaseAuthService>();

            return services;
        }
    }

}
