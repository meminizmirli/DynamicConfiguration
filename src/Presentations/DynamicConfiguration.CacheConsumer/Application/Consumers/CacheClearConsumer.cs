using System.Threading.Tasks;
using DynamicConfiguration.Bus.Contracts.Commands;
using DynamicConfiguration.CacheConsumer.Domain.Abstraction.Data;
using MassTransit;

namespace DynamicConfiguration.CacheConsumer.Application.Consumers
{
    public class CacheClearConsumer : IConsumer<ICacheClearCommand>
    {
        private readonly IRedisDataPort _redisDataPort;

        public CacheClearConsumer(IRedisDataPort redisDataPort)
        {
            _redisDataPort = redisDataPort;
        }

        public async Task Consume(ConsumeContext<ICacheClearCommand> context)
        {
            var command = context.Message;
            var result = await _redisDataPort.ClearCache(command.RedisKey);
        }
    }
}