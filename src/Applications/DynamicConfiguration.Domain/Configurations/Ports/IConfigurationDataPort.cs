using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamicConfiguration.Domain.Configurations.Ports
{
    public interface IConfigurationDataPort
    {
        Task<List<Configuration>> ListAsync();
        Task<List<ConfigurationRedisDto>> ListForRedisAsync(string aplicationName);
        Task<Configuration> GetByIdAsync(string id);
        Task<bool> CreateAsync(Configuration configuration);
        Task<bool> UpdateAsync(Configuration configuration);
        Task<bool> RemoveAsync(Configuration configuration);
    }
}
