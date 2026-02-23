using System.Reflection;
using Coffee_Machine.Application.Helper;
using Coffee_Machine.Application.Interface;

namespace Coffee_Machine.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Use the new MediatR registration method
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IBrewCounter, BrewCounter>();
            services.AddHttpClient();

            services.AddTransient<IWeatherService>(sp =>
            {
                var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient();
                httpClient.BaseAddress = new Uri("https://api.openweathermap.org/");

                var configuration = sp.GetRequiredService<IConfiguration>();

                return new WeatherMapService(httpClient, configuration);
            });

            return services;
        }
    }
}
