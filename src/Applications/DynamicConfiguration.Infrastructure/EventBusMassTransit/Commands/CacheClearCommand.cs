using DynamicConfiguration.Bus.Contracts.Commands;

namespace DynamicConfiguration.Infrastructure.EventBusMassTransit.Commands
{
    public class CacheClearCommand : ICacheClearCommand
    {
        public CacheClearCommand(string redisKey)
        {
            RedisKey = redisKey;
        }
        public string RedisKey { get; set; }
    }
}
