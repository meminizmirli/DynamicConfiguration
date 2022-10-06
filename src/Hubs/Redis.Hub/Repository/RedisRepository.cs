using System;
using System.Text.Json;
using System.Threading.Tasks;
using Redis.Hub.Connection;
using Redis.Hub.Extensions;
using StackExchange.Redis;

namespace Redis.Hub.Repository
{
    internal class RedisRepository : IRedisRepository
    {
        private readonly IRedisConnectionFactory _redisConnectionFactory;
        private readonly IConnectionMultiplexer _connection;
        private readonly IDatabase _cache;
        private readonly bool _hasCache;

        public RedisRepository(IRedisConnectionFactory redisConnectionFactory)
        {
            _redisConnectionFactory = redisConnectionFactory;
            _connection = _redisConnectionFactory?.Connect();
            if(_connection?.IsConnected ?? false)
                _cache = _connection.GetDatabase();
            _hasCache = _cache != null;
        }
        
        public async Task<T> GetAsync<T>(string key)
        {
            var result = default(T);
            try
            {
                if (_hasCache)
                {
                    string data = await _cache.StringGetAsync(key, CommandFlags.PreferReplica);
                    result = data != null
                        ? JsonSerializer.Deserialize<T>(data)
                        : default(T);
                }

                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }
        public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> action, TimeSpan expiration)
        {
            var result = default(T);
            try
            {
                if (_hasCache)
                {
                    string data = await _cache.StringGetAsync(key, CommandFlags.PreferReplica);
                    if (string.IsNullOrWhiteSpace(data))
                    {
                        result = await action();
                        await SetAsync<T>(key, result, expiration);
                    }
                    else
                    {
                        result = JsonSerializer.Deserialize<T>(data);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
        public async Task<bool> SetAsync<T>(string key, T value)
        {
            bool result = false;
            try
            {
                if (_hasCache)
                {
                    result = await _cache.StringSetAsync(key, JsonSerializer.Serialize(value), flags: CommandFlags.PreferMaster);
                }
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }
        public async Task<bool> SetAsync<T>(string key, T value, DateTime expirationDate)
        {
            bool result = false;
            var timeSpanExpiration = expirationDate.ToTimeSpan();
            if (timeSpanExpiration.TotalMilliseconds <= 0)
                return result;

            try
            {
                if (_hasCache)
                {
                    result = await _cache.StringSetAsync(key, JsonSerializer.Serialize(value), timeSpanExpiration, When.Always, CommandFlags.PreferMaster);
                }

                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }
        public async Task<bool> SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            bool result = false;
            try
            {
                if (_hasCache)
                {
                    result = await _cache.StringSetAsync(key, JsonSerializer.Serialize(value), expiration, When.Always, CommandFlags.PreferMaster);
                }

                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }
        public async Task<bool> KeyExistsAsync(string key)
        {
            bool result = false;
            try
            {
                if (_hasCache)
                {
                    result = await _cache.KeyExistsAsync(key, CommandFlags.PreferReplica);
                }
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }
        public async Task<bool> RemoveAsync(string key)
        {
            bool result = false;

            try
            {
                if (_hasCache)
                {
                    result = await _cache.KeyDeleteAsync(key, CommandFlags.PreferMaster);
                }

                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        public async Task<bool> PingAsync()
        {
            try
            {
                if (!_connection?.IsConnected ?? false)
                    return false;

                await _cache.PingAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}