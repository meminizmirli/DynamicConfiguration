using DynamicConfiguration.Domain.Caches.Ports;
using DynamicConfiguration.Domain.Configurations.Ports;
using DynamicConfiguration.Infrastructure.EventBusMassTransit;
using DynamicConfiguration.Infrastructure.EventBusMassTransit.Adapters;
using DynamicConfiguration.Infrastructure.Mongo.Configurations.Adapters;
using DynamicConfiguration.Infrastructure.Mongo.Configurations.Repositories;
using DynamicConfiguration.Infrastructure.Redis.Configurations.Adapters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mongo.Hub;
using Redis.Hub;
using Redis.Hub.Repository;

namespace DynamicConfiguration.Infrastructure
{
    public static class StartupSetup
    {
        private static readonly InfrastructureSettings _infrastructureSettings = new InfrastructureSettings();
        public static IServiceCollection AddDynamicConfigurationInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.addConfigurations(configuration);
            services.addPorts(configuration);
            services.addRepositories(configuration);

            return services;
        }

        private static IServiceCollection addConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongoHub(configuration);
            services.AddRedisHub(configuration);
            services.AddEventBusMassTransit(configuration);
            return services;
        }

        private static IServiceCollection addPorts(this IServiceCollection services, IConfiguration configuration)
        {
            configuration.Bind("InfrastructureSettings", _infrastructureSettings);
            services.AddScoped<ICacheEventBusPort, CacheMassTransitAdapter>();
            services.AddScoped<IConfigurationDataPort, ConfigurationMongoAdapter>();
            services.AddScoped<IConfigurationRedisDataPort>(x => new ConfigurationRedisAdapter(
                redisRepository: x.GetService<IRedisRepository>(),
                configurationRepository: x.GetService<IConfigurationDataPort>(),
                cacheEventBusPort: x.GetService<ICacheEventBusPort>(),
                applicationName: _infrastructureSettings.ApplicationName,
                refreshTimerIntervalInMs: _infrastructureSettings.RefreshTimerIntervalInMs));
            return services;
        }
        private static IServiceCollection addRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            return services;
        }
    }
}
