using System.Threading.Tasks;
using Mongo.Hub.Extensions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Mongo.Hub.Context
{
    internal class MongoHubContext : IMongoHubContext
    {
        private readonly string _connectionStrings;

        public MongoHubContext(string connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }

        public IMongoDatabase GetDB<T>() where T : class
        {
            var url = new MongoUrl(_connectionStrings);
            var client = new MongoClient(url);
            IMongoDatabase db;
            var readPreference = MongoDbContextExtension.GetReadPreference<T>();


            if (!string.IsNullOrWhiteSpace(readPreference))
            {
                MongoDatabaseSettings settings = new MongoDatabaseSettings();
                if (readPreference.Equals("Primary"))
                    settings.ReadPreference = ReadPreference.Primary;

                db = client.GetDatabase(url.DatabaseName, settings);
            }
            else
            {
                db = client.GetDatabase(url.DatabaseName);
            }
            return db;
        }

        public IMongoCollection<T> GetCollection<T>() where T : class
        {
            var db = GetDB<T>();
            var isExist = CollectionExists<T>(db);
            if (!isExist)
                CreateCollection<T>(db);
            var collection = db.GetCollection<T>(MongoDbContextExtension.GetCollectionName<T>());
            return collection;
        }

        public bool CollectionExists<T>(IMongoDatabase db) where T : class
        {
            var filter = new BsonDocument("name", MongoDbContextExtension.GetCollectionName<T>());
            var options = new ListCollectionNamesOptions { Filter = filter };

            return db.ListCollectionNames(options).Any();
        }

        private void CreateCollection<T>(IMongoDatabase db) where T : class
        {
            db.CreateCollection(MongoDbContextExtension.GetCollectionName<T>());
            object instance = System.Activator.CreateInstance(typeof(T));
            var entity = (T)instance;
            db.GetCollection<T>(MongoDbContextExtension.GetCollectionName<T>()).InsertOne(entity);
        }

        public async Task<bool> PingAsync()
        {
            try
            {
                var url = new MongoUrl(_connectionStrings);
                var client = new MongoClient(url);
                await client.GetDatabase(url.DatabaseName).ListCollectionNamesAsync();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}