using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicConfiguration.Core.Infrastructure.Data;
using Mongo.Hub.Context;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DynamicConfiguration.Infrastructure.Mongo
{
    public class BaseRepository<T> : IRepository<T, string> where T : _Entity<string>
    {
        protected readonly IMongoHubContext _mongoHubContext;

        public BaseRepository(IMongoHubContext mongoHubContext)
        {
            _mongoHubContext = mongoHubContext;
        }

        public virtual async Task<T> GetByIdAsync(string id)
        {
            return (await _mongoHubContext.GetCollection<T>().FindAsync(Builders<T>.Filter.Eq("_id", ObjectId.Parse(id.ToString())))).FirstOrDefault();
        }
        public virtual async Task<bool> CreateAsync(T entity)
        {
            await _mongoHubContext.GetCollection<T>().InsertOneAsync(entity);
            return true;
        }
        public virtual async Task<bool> DeleteAsync(T entity)
        {
            await _mongoHubContext.GetCollection<T>().FindOneAndDeleteAsync(Builders<T>.Filter.Eq("_id", ObjectId.Parse(entity.Id.ToString())));
            return true;
        }
        public virtual async Task<bool> UpdateAsync(T entity)
        {
            var res = await _mongoHubContext.GetCollection<T>().ReplaceOneAsync(q => q.Id == entity.Id, entity);
            return true;
        }
        public virtual async Task<bool> BulkCreateAsync(List<T> entities)
        {
            await _mongoHubContext.GetCollection<T>().InsertManyAsync(entities);
            return true;
        }
        public virtual async Task<bool> BulkUpdateAsync(List<T> entities)
        {
            foreach (var entity in entities)
            {
                await _mongoHubContext.GetCollection<T>().ReplaceOneAsync(q => q.Id == entity.Id, entity);
            }
            return true;
        }
    }
}