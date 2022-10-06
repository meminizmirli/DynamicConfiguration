using System;
using StackExchange.Redis;

namespace Redis.Hub.Connection
{
    internal class RedisConnectionFactory : IRedisConnectionFactory
    {
        private readonly string _connectionStrings;

        public RedisConnectionFactory(string connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }
        
        public ConnectionMultiplexer Connect()
        {
            var connectionMultiplexer = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(_connectionStrings));
            return connectionMultiplexer.Value;
        }
    }
}