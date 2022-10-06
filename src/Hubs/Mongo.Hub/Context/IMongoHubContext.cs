using System.Threading.Tasks;
using MongoDB.Driver;

namespace Mongo.Hub.Context
{
    public interface IMongoHubContext
    {
        IMongoDatabase GetDB<T>() where T : class;
        IMongoCollection<T> GetCollection<T>() where T : class;
        Task<bool> PingAsync();
    }
}