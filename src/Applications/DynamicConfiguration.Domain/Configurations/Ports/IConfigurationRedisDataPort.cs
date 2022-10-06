using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamicConfiguration.Domain.Configurations.Ports
{
    public interface IConfigurationRedisDataPort
    {
        Task<List<ConfigurationRedisDto>> ListAsync();
        Task<ConfigurationRedisDto> GetAsync(string name);
        Task<T> GetAsync<T>(string name);
        Task ClearCacheAsync();
    }
}