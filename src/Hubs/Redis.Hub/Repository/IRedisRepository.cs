using System;
using System.Threading.Tasks;

namespace Redis.Hub.Repository
{
    public interface IRedisRepository
    {
        Task<T> GetAsync<T>(string key);
        Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> action, TimeSpan expiration);
        Task<bool> SetAsync<T>(string key, T value);
        Task<bool> SetAsync<T>(string key, T value, DateTime expirationDate);
        Task<bool> SetAsync<T>(string key, T value, TimeSpan expiration);
        Task<bool> RemoveAsync(string key);
        Task<bool> KeyExistsAsync(string key);
        Task<bool> PingAsync();
    }
}