using Microsoft.Extensions.DependencyInjection;

namespace DynamicConfiguration.Bus
{
    public static class StartupSetup
    {
        public static IServiceCollection AddDynamicConfigurationBus(this IServiceCollection services)
        {
            services.AddScoped<IDynamicConfigurationBusPublisher, DynamicConfigurationBus>();
            return services;
        }
    }
}