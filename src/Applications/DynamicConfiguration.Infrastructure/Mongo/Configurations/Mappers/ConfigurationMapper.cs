using System.Collections.Generic;
using DynamicConfiguration.Domain.Configurations;
using DynamicConfiguration.Infrastructure.Mongo.Configurations.Entities;

namespace DynamicConfiguration.Infrastructure.Mongo.Configurations.Mappers
{
    public static partial class ConfigurationMapper
    {
        public static ConfigurationEntity ToEntity(this Configuration configuration)
        {
            if (configuration == null) return null;

            return new ConfigurationEntity
            {
                Id = configuration.Id,
                IsDeleted = configuration.IsDeleted,
                CreatedAt = configuration.CreatedAt,
                UpdatedAt = configuration.UpdatedAt,
                Status = configuration.Status,
                Name = configuration.Name,
                Type = configuration.Type.Key,
                Value = configuration.Value,
                ApplicationName = configuration.ApplicationName
            };
        }
    }

    public static partial class ConfigurationMapper
    {
        public static Configuration ToDomain(this ConfigurationEntity configurationEntity)
        {
            if (configurationEntity == null) return null;

            return Configuration.Map(
                id: configurationEntity.Id,
                isDeleted: configurationEntity.IsDeleted,
                createdAt: configurationEntity.CreatedAt,
                updatedAt: configurationEntity.UpdatedAt,
                status: configurationEntity.Status,
                name: configurationEntity.Name,
                type: configurationEntity.Type,
                value: configurationEntity.Value,
                applicationName: configurationEntity.ApplicationName);
        }

        public static ConfigurationRedisDto ToRedisDto(this ConfigurationEntity configurationEntity)
        {
            if (configurationEntity == null) return null;

            return ConfigurationRedisDto.Map(
                name: configurationEntity.Name,
                type: configurationEntity.Type,
                value: configurationEntity.Value,
                applicationName: configurationEntity.ApplicationName);
        }
    }

    public static partial class ConfigurationMapper
    {
        public static void WriteChanges(this Configuration configuration, ConfigurationEntity configurationEntity)
        {
            configuration.SetChanges(id: configurationEntity.Id);
        }
        public static void WriteChanges(this List<Configuration> configurations, List<ConfigurationEntity> configurationEntities)
        {
            for (int i = 0; i < configurations.Count; i++)
                configurations[i].WriteChanges(configurationEntities[i]);
        }
    }
}
