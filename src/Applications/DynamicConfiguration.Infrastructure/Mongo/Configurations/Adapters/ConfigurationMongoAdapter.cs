using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DynamicConfiguration.Domain.Configurations;
using DynamicConfiguration.Domain.Configurations.Ports;
using DynamicConfiguration.Infrastructure.Mongo.Configurations.Mappers;
using DynamicConfiguration.Infrastructure.Mongo.Configurations.Repositories;

namespace DynamicConfiguration.Infrastructure.Mongo.Configurations.Adapters
{
    public class ConfigurationMongoAdapter : IConfigurationDataPort
    {
        private readonly IConfigurationRepository _configurationRepository;

        public ConfigurationMongoAdapter(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        public async Task<List<Configuration>> ListAsync()
        {
            var configurations = (await _configurationRepository.ListAsync())?.Select(x => x.Map()).ToList();
            return configurations;
        }

        public async Task<List<ConfigurationRedisDto>> ListForRedisAsync(string aplicationName)
        {
            var configurations = (await _configurationRepository.ListAsync(aplicationName))?.Select(x => x.MapToRedisDto()).ToList();
            return configurations;
        }

        public async Task<Configuration> GetByIdAsync(string id)
        {
            var configuration = (await _configurationRepository.GetByIdAsync(id))?.ToDomain();
            return configuration;
        }

        public async Task<bool> CreateAsync(Configuration configuration)
        {
            var configurationEntity = configuration.Create();
            bool result = await _configurationRepository.CreateAsync(configurationEntity);
            configuration.WriteChanges(configurationEntity);
            return result;
        }

        public async Task<bool> UpdateAsync(Configuration configuration)
        {
            var configurationEntity = configuration.Update();
            bool result = await _configurationRepository.UpdateAsync(configurationEntity);
            return result;
        }

        public async Task<bool> RemoveAsync(Configuration configuration)
        {
            var configurationEntity = configuration.Update();
            bool result = await _configurationRepository.UpdateAsync(configurationEntity);
            return result;
        }
    }
}
