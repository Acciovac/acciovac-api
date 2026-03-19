using acciovac.Application.Abstractions;
using acciovac.Infrastructure.Persistence;
using acciovac.Infrastructure.Repositories;
using acciovac.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddHttpClient<IGoogleMapsService, GoogleMapsService>();
            services.AddHttpClient<IWeatherService, WeatherService>();

            return services;
        }
    }
}
