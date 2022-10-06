using System.Threading;
using System.Threading.Tasks;
using DynamicConfiguration.Core.Application.Abstractions.Mediator;
using DynamicConfiguration.Domain.Configurations.Ports;

namespace DynamicConfiguration.Application.Configurations.Events.CacheClearConfiguration
{
    public class CacheClearConfigurationEventHandler : IEventHandler<CacheClearConfigurationEvent>
    {
        private readonly IConfigurationRedisDataPort _configurationRedisDataPort;

        public CacheClearConfigurationEventHandler(IConfigurationRedisDataPort configurationRedisDataPort)
        {
            _configurationRedisDataPort = configurationRedisDataPort;
        }

        public async Task Handle(CacheClearConfigurationEvent notification, CancellationToken cancellationToken)
        {
            await _configurationRedisDataPort.ClearCacheAsync();
        }
    }
}