using System.Reflection;
using Coffee_Machine.Application.Helper;
using Coffee_Machine.Application.Interface;

namespace Coffee_Machine.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Use the new MediatR registration method
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IBrewCounter, BrewCounter>();

            return services;
        }
    }
}
