using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicConfiguration.Core.Infrastructure.Data;
using DynamicConfiguration.Infrastructure.Mongo.Configurations.Entities;

namespace DynamicConfiguration.Infrastructure.Mongo.Configurations.Repositories
{
    public interface IConfigurationRepository : IRepository<ConfigurationEntity, string>
    {
        Task<List<ConfigurationEntity>> ListAsync();
        Task<List<ConfigurationEntity>> ListAsync(string aplicationName);
    }
}
