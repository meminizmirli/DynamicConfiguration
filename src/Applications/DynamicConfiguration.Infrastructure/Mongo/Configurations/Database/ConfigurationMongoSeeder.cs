using System.Collections.Generic;
using DynamicConfiguration.Core.Domain.Constants;
using DynamicConfiguration.Domain.Configurations.Values;
using DynamicConfiguration.Infrastructure.Mongo.Configurations.Entities;
using Mongo.Hub.Context;
using MongoDB.Driver;

namespace DynamicConfiguration.Infrastructure.Mongo.Configurations.Database
{
    public class ConfigurationMongoSeeder
    {
        private readonly IMongoHubContext _mongoHubContext;

        public ConfigurationMongoSeeder(IMongoHubContext mongoHubContext)
        {
            _mongoHubContext = mongoHubContext;
        }

        public void SeedOnProgramRun()
        {
            SeedConfigurations();
        }
        private void SeedConfigurations()
        {
            var collection = _mongoHubContext.GetCollection<ConfigurationEntity>();

            var status_indexKeysDefinition = Builders<ConfigurationEntity>.IndexKeys.Ascending(x => x.Status);
            collection.Indexes.CreateOne(new CreateIndexModel<ConfigurationEntity>(status_indexKeysDefinition));

            collection.DeleteMany(Builders<ConfigurationEntity>.Filter.Eq(x=> x.Status, 0));
            var configurationEntityCount = collection.CountDocuments(Builders<ConfigurationEntity>.Filter.Empty);

            if (configurationEntityCount > 0) return;

            collection.InsertMany(Configurations);
        }

        private List<ConfigurationEntity> Configurations => new List<ConfigurationEntity>
        {
            new ConfigurationEntity
            {
                IsDeleted = false,
                Status = BaseStatus.Active,
                Name = "SiteName",
                Type = PropertyType.String.Key,
                Value = "Boyner.com.tr",
                ApplicationName = "DynamicConfiguration.API"
            },
            new ConfigurationEntity
            {
                IsDeleted = false,
                Status = BaseStatus.Active,
                Name = "IsBasketEnabled",
                Type = PropertyType.Boolean.Key,
                Value = "1",
                ApplicationName = "DynamicConfiguration.WEB"
            },
            new ConfigurationEntity
            {
                IsDeleted = false,
                Status = BaseStatus.Passive,
                Name = "MaxItemCount",
                Type = PropertyType.Integer.Key,
                Value = "50",
                ApplicationName = "DynamicConfiguration.API"
            },
        };
    }

}
