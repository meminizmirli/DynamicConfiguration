using System;
using DynamicConfiguration.Application.Base.Dtos;
using DynamicConfiguration.Domain.Configurations;

namespace DynamicConfiguration.Application.Configurations.Dtos
{
    public class ConfigurationDto : BaseActivityDto<string>
    {
        public ConfigurationDto() { }

        public ConfigurationDto(string id, int status, DateTime createdAt, DateTime updatedAt) : base(id, status, createdAt, updatedAt)
        {
        }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string ApplicationName { get; set; }

        public static ConfigurationDto Map(Configuration configuration)
        {
            return new ConfigurationDto(configuration.Id, configuration.Status, configuration.CreatedAt, configuration.UpdatedAt)
            {
                Name = configuration.Name,
                Type = configuration.Type.DisplayName,
                Value = configuration.Value,
                ApplicationName = configuration.ApplicationName
            };
        }
    }
}
