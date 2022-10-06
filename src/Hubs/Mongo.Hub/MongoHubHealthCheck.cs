using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Mongo.Hub.Context;

namespace Mongo.Hub
{
    public class MongoHubHealthCheck : IHealthCheck
    {
        private readonly IMongoHubContext _mongoHubContext;
        public MongoHubHealthCheck(IMongoHubContext mongoHubContext)
        {
            _mongoHubContext = mongoHubContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return await _mongoHubContext.PingAsync()
                    ? HealthCheckResult.Healthy($"{context.Registration.Name}: success")
                    : new HealthCheckResult(context.Registration.FailureStatus);
        }
    }
}