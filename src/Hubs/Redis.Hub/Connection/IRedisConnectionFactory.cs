using StackExchange.Redis;

namespace Redis.Hub.Connection
{
    public interface IRedisConnectionFactory
    {
        ConnectionMultiplexer Connect();
    }
}