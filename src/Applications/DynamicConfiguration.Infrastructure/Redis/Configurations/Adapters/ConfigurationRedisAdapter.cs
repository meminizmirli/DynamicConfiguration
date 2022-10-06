using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DynamicConfiguration.Domain.Caches.Ports;
using DynamicConfiguration.Domain.Configurations;
using DynamicConfiguration.Domain.Configurations.Ports;
using DynamicConfiguration.Domain.Configurations.Values;
using Redis.Hub.Repository;

namespace DynamicConfiguration.Infrastructure.Redis.Configurations.Adapters
{
    public class ConfigurationRedisAdapter : IConfigurationRedisDataPort
    {
        private const string ConfigurationRedisKey = "CONFIGURATION";
        private readonly string _applicationName;
        private readonly IRedisRepository _redisRepository;
        private readonly IConfigurationDataPort _configurationRepository;
        private readonly ICacheEventBusPort _cacheEventBusPort;
        private readonly ConfigurationRedisKey _configurationRedisKey;
        private readonly TimeSpan _refreshTimerIntervalInMs;

        public ConfigurationRedisAdapter(IRedisRepository redisRepository, IConfigurationDataPort configurationRepository, ICacheEventBusPort cacheEventBusPort, string applicationName, TimeSpan refreshTimerIntervalInMs)
        {
            _redisRepository = redisRepository;
            _configurationRepository = configurationRepository;
            _cacheEventBusPort = cacheEventBusPort;
            _applicationName = applicationName;
            _configurationRedisKey = Domain.Configurations.Values.ConfigurationRedisKey.ToPropertyType(_applicationName);
            _refreshTimerIntervalInMs = refreshTimerIntervalInMs;
        }

        public async Task<List<ConfigurationRedisDto>> ListAsync()
        {
            var key = $"{_configurationRedisKey.RedisKey}_{ConfigurationRedisKey}";
            var configurations = await _redisRepository.GetOrAddAsync<List<ConfigurationRedisDto>>(
                key: key,
                action: async () => await _configurationRepository.ListForRedisAsync(_applicationName),
                expiration: _refreshTimerIntervalInMs);
            return configurations;
        }

        public async Task<ConfigurationRedisDto> GetAsync(string name)
        {
            var configurations = await ListAsync();
            var configuration = configurations?.FirstOrDefault(x => x.Name == name);
            return configuration;
        }

        public async Task<T> GetAsync<T>(string name)
        {
            var configurations = await ListAsync();
            var configuration = configurations?.FirstOrDefault(x => x.Name == name);
            if (configuration == null) throw new KeyNotFoundException();
            var value = configuration.Type.ToParse(configuration.Value);
            if (value.GetType() == typeof(T))
                return (T)value;
            throw new KeyNotFoundException();
        }

        public async Task ClearCacheAsync()
        {
            var key = $"{_configurationRedisKey.RedisKey}_{ConfigurationRedisKey}";
            await _cacheEventBusPort.CacheClearAsync(key);
        }
    }
}
