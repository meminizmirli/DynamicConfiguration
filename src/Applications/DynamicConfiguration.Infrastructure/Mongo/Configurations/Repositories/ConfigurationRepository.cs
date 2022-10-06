using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicConfiguration.Core.Domain.Constants;
using DynamicConfiguration.Infrastructure.Mongo.Configurations.Entities;
using DynamicConfiguration.Infrastructure.Mongo.Configurations.Filters;
using Mongo.Hub.Context;
using MongoDB.Driver;

namespace DynamicConfiguration.Infrastructure.Mongo.Configurations.Repositories
{
    public class ConfigurationRepository : BaseRepository<ConfigurationEntity>, IConfigurationRepository
    {
        public ConfigurationRepository(IMongoHubContext mongoHubContext) : base(mongoHubContext)
        {
        }

        public async Task<List<ConfigurationEntity>> ListAsync()
        {
            var filter = new ConfigurationMongoFilter()._BaseFilter();
            var orderPackages = await (await _mongoHubContext.GetCollection<ConfigurationEntity>().FindAsync<ConfigurationEntity>(filter)).ToListAsync();

            return orderPackages;
        }

        public async Task<List<ConfigurationEntity>> ListAsync(string aplicationName)
        {
            var filter = new ConfigurationMongoFilter()._ApplicationNameFilter(aplicationName, BaseStatus.Active);
            var orderPackages = await (await _mongoHubContext.GetCollection<ConfigurationEntity>().FindAsync<ConfigurationEntity>(filter)).ToListAsync();

            return orderPackages;
        }

        public override async Task<ConfigurationEntity> GetByIdAsync(string id)
        {
            var filter = new ConfigurationMongoFilter()._IdFilter(id);
            var orderPackage = await (await _mongoHubContext.GetCollection<ConfigurationEntity>().FindAsync<ConfigurationEntity>(filter)).FirstOrDefaultAsync();

            return orderPackage;
        }
    }
}