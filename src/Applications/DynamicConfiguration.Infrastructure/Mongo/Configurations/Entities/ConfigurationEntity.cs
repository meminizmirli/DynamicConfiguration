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

        public static ConfigurationEntity Map(Configuration configuration)
        {
            if (configuration == null) return null;

            return new ConfigurationEntity(
                id: configuration.Id,
                isDeleted: configuration.IsDeleted,
                createdAt: configuration.CreatedAt,
                updatedAt: configuration.UpdatedAt,
                status: configuration.Status,
                name: configuration.ApplicationName,
                type: configuration.Type.Key,
                value: configuration.Value,
                applicationName: configuration.ApplicationName);
        }

        public Configuration ToDomain()
        {
            return Configuration.Map(
                id: this.Id,
                isDeleted: this.IsDeleted,
                createdAt: this.CreatedAt,
                updatedAt: this.UpdatedAt,
                status: this.Status,
                name: this.Name,
                type: this.Type,
                value: this.Value,
                applicationName: this.ApplicationName);
        }
        public ConfigurationRedisDto ToRedisDto()
        {
            return ConfigurationRedisDto.Map(
                name: this.Name,
                type: this.Type,
                value: this.Value,
                applicationName: this.ApplicationName);
        }
    }
}