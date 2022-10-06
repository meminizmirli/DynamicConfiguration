using System.Threading.Tasks;
using DynamicConfiguration.CacheConsumer.Domain.Abstraction.Data;
using Redis.Hub.Repository;

namespace DynamicConfiguration.CacheConsumer.Infrastructure.Redis
{
    public class RedisAdapter : IRedisDataPort
    {
        private readonly IRedisRepository _redisRepository;

        public RedisAdapter(IRedisRepository redisRepository)
        {
            _redisRepository = redisRepository;
        }

        public async Task<bool> ClearCache(string redisKey)
        {
            var removeResult = await _redisRepository.RemoveAsync(redisKey);
            return removeResult;
        }
    }
}
