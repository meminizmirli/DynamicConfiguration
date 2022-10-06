using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicConfiguration.Bus;
using DynamicConfiguration.Domain.Caches.Ports;
using DynamicConfiguration.Infrastructure.EventBusMassTransit.Commands;

namespace DynamicConfiguration.Infrastructure.EventBusMassTransit.Adapters
{
    public class CacheMassTransitAdapter : ICacheEventBusPort
    {
        private readonly IDynamicConfigurationBusPublisher _dynamicConfigurationBusPublisher;

        public CacheMassTransitAdapter(IDynamicConfigurationBusPublisher dynamicConfigurationBusPublisher)
        {
            _dynamicConfigurationBusPublisher = dynamicConfigurationBusPublisher;
        }

        public async Task CacheClearAsync(string redisKey)
        {
            await _dynamicConfigurationBusPublisher.Publish(new CacheClearCommand(redisKey));
        }
    }
}
