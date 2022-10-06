using DynamicConfiguration.CacheConsumer.Domain.Abstraction.Data;
using DynamicConfiguration.CacheConsumer.Infrastructure.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redis.Hub;

namespace DynamicConfiguration.CacheConsumer.Infrastructure
{
    public static class StartupSetup
    {
        public static IServiceCollection AddDynamicConfigurationCacheConsumerInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRedisHub(configuration);
            services.AddScoped<IRedisDataPort, RedisAdapter>();

            return services;
        }
    }
}
