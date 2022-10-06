using System.Collections.Generic;
using DynamicConfiguration.Domain.Configurations;
using DynamicConfiguration.Infrastructure.Mongo.Configurations.Entities;

namespace DynamicConfiguration.Infrastructure.Mongo.Configurations.Mappers
{
    public static partial class ConfigurationMapper
    {
        public static ConfigurationEntity Map(this Configuration orderPackage) => ConfigurationEntity.Map(orderPackage);
        public static Configuration Map(this ConfigurationEntity orderPackageEntity) => orderPackageEntity?.ToDomain();
        public static ConfigurationRedisDto MapToRedisDto(this ConfigurationEntity orderPackageEntity) => orderPackageEntity?.ToRedisDto();
    }

    public static partial class ConfigurationMapper
    {
        public static ConfigurationEntity Create(this Configuration configuration)
        {
            return new ConfigurationEntity(
                name: configuration.Name,
                type: configuration.Type.Key,
                value: configuration.Value,
                applicationName: configuration.ApplicationName,
                status: configuration.Status);
        }
    }

    public static partial class ConfigurationMapper
    {
        public static ConfigurationEntity Update(this Configuration configuration)
        {
            return new ConfigurationEntity(
                id: configuration.Id,
                isDeleted: configuration.IsDeleted,
                createdAt: configuration.CreatedAt,
                updatedAt: configuration.UpdatedAt,
                status: configuration.Status,
                name: configuration.Name,
                type: configuration.Type.Key,
                value: configuration.Value,
                applicationName: configuration.ApplicationName);
        }
    }

    public static partial class ConfigurationMapper
    {
        public static void WriteChanges(this Configuration linkedAccount, ConfigurationEntity linkedAccountEntity)
        {
            linkedAccount.SetChanges(id: linkedAccountEntity.Id);
        }
        public static void WriteChanges(this List<Configuration> linkedAccounts, List<ConfigurationEntity> linkedAccountEntities)
        {
            for (int i = 0; i < linkedAccounts.Count; i++)
                linkedAccounts[i].WriteChanges(linkedAccountEntities[i]);
        }
    }
}
