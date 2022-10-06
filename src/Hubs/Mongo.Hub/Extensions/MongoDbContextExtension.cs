using System.Reflection;
using Mongo.Hub.Attributes;

namespace Mongo.Hub.Extensions
{
    public static class MongoDbContextExtension
    {
        public static string GetConnectionStringName<T>() where T : class
        {
            var t = typeof(T);
            var a = t.GetCustomAttribute<MongoDBCollectionAttribute>();
            var cn = (a != null && !string.IsNullOrEmpty(a.ConnectionStringName)) ? a.ConnectionStringName : "Default";
            return cn;
        }

        public static string GetCollectionName<T>() where T : class
        {
            var t = typeof(T);
            var a = t.GetCustomAttribute<MongoDBCollectionAttribute>();
            var cn = (a != null && !string.IsNullOrEmpty(a.CollectionName)) ? a.CollectionName : t.Name;
            return cn;
        }

        public static string GetReadPreference<T>() where T : class
        {
            var t = typeof(T);
            var a = t.GetCustomAttribute<MongoDBCollectionAttribute>();
            return a.ReadPreference;
        }
    }
}