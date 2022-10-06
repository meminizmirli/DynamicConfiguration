using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redis.Hub.Connection;
using Redis.Hub.Repository;

namespace Redis.Hub
{
    public static class StartupSetup
    {
        private static readonly RedisHubSettings _redisHubSettings = new RedisHubSettings();

        public static IServiceCollection AddRedisHub(this IServiceCollection services, IConfiguration configuration)
        {
            configuration.Bind("RedisHubSettings", _redisHubSettings);
            services.AddSingleton(_redisHubSettings);
            services.AddSingleton<IRedisConnectionFactory, RedisConnectionFactory>(x => new RedisConnectionFactory(_redisHubSettings.ConnectionStrings));
            services.AddSingleton<IRedisRepository, RedisRepository>();
            services.AddHealthChecks().AddCheck<RedisHubHealthCheck>("Redis.Hub");

            return services;
        }
    }
}