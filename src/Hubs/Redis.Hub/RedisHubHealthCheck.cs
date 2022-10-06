using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Redis.Hub.Repository;

namespace Redis.Hub
{
    public class RedisHubHealthCheck : IHealthCheck
    {
        private readonly IRedisRepository _redisRepository;
        public RedisHubHealthCheck(IRedisRepository redisRepository)
        {
            _redisRepository = redisRepository;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return await _redisRepository.PingAsync()
                    ? HealthCheckResult.Healthy($"{context.Registration.Name}: success")
                    : new HealthCheckResult(context.Registration.FailureStatus);
        }
    }
}