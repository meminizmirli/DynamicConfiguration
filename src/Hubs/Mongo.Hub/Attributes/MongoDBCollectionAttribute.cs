using System;

namespace Mongo.Hub.Attributes
{
    public class MongoDBCollectionAttribute : Attribute
    {
        public string CollectionName { get; set; }
        public string ConnectionStringName { get; set; }
        public string ReadPreference { get; set; }
    }
}