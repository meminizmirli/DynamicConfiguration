using System.Threading;
using System.Threading.Tasks;
using DynamicConfiguration.Core.Application.Abstractions.Mediator;
using DynamicConfiguration.Core.Application.Handlers;
using DynamicConfiguration.Core.Application.Models;
using DynamicConfiguration.Domain.Configurations.Ports;

namespace DynamicConfiguration.Application.Configurations.Queries.GetConfigurationFromRedis
{
    public class GetConfigurationFromRedisQueryHandler : AppResponseHandler<string>
        , IQueryHandler<GetConfigurationStringFromRedisQuery, string>
        , IQueryHandler<GetConfigurationIntFromRedisQuery, string>
        , IQueryHandler<GetConfigurationBoolFromRedisQuery, string>
        , IQueryHandler<GetConfigurationDoubleFromRedisQuery, string>
    {
        private readonly IConfigurationRedisDataPort _configurationRedisDataPort;

        public GetConfigurationFromRedisQueryHandler(IConfigurationRedisDataPort configurationRedisDataPort)
        {
            _configurationRedisDataPort = configurationRedisDataPort;
        }

        public async Task<AppResponse<string>> Handle(GetConfigurationStringFromRedisQuery request, CancellationToken cancellationToken)
        {
            var value = await _configurationRedisDataPort.GetAsync<string>(request.Name);
            return OK(value);
        }

        public async Task<AppResponse<string>> Handle(GetConfigurationIntFromRedisQuery request, CancellationToken cancellationToken)
        {
            var value = await _configurationRedisDataPort.GetAsync<int>(request.Name);
            return OK(value.ToString());
        }

        public async Task<AppResponse<string>> Handle(GetConfigurationBoolFromRedisQuery request, CancellationToken cancellationToken)
        {
            var value = await _configurationRedisDataPort.GetAsync<bool>(request.Name);
            return OK(value.ToString());
        }

        public async Task<AppResponse<string>> Handle(GetConfigurationDoubleFromRedisQuery request, CancellationToken cancellationToken)
        {
            var value = await _configurationRedisDataPort.GetAsync<double>(request.Name);
            return OK(value.ToString());
        }
    }
}