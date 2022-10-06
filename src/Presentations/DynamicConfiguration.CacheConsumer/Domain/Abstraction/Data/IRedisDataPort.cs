using System.Threading.Tasks;

namespace DynamicConfiguration.CacheConsumer.Domain.Abstraction.Data
{
    public interface IRedisDataPort
    {
        Task<bool> ClearCache(string redisKey);
    }
}
