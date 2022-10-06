using System;
using DynamicConfiguration.Domain.Configurations;
using Mongo.Hub.Attributes;

namespace DynamicConfiguration.Infrastructure.Mongo.Configurations.Entities
{
    [MongoDBCollection(CollectionName = "Configurations")]
    public class ConfigurationEntity : _ActivityEntity<string>
    {
        public ConfigurationEntity() { }
        public ConfigurationEntity(string name, string type, string value, string applicationName, int status)
        {
            Name = name;
            Type = type;
            Value = value;
            ApplicationName = applicationName;
            Status = status;
        }

        public ConfigurationEntity(string id, bool isDeleted, DateTime createdAt, DateTime updatedAt, int status, string name, string type, string value, string applicationName) : base(id, isDeleted, createdAt, updatedAt, status)
        {
            Name = name;
            Type = type;
            Value = value;
            ApplicationName = applicationName;
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string ApplicationName { get; set; }
    }
}