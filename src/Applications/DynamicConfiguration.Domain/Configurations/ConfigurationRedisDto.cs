using DynamicConfiguration.Domain.Configurations.Values;

namespace DynamicConfiguration.Domain.Configurations
{
    public class ConfigurationRedisDto
    {
        public string Name { get; set; }
        public PropertyType Type { get; set; }
        public string Value { get; set; }
        public string ApplicationName { get; set; }
        public static ConfigurationRedisDto Map(
            string name,
            string type,
            string value,
            string applicationName)
        {
            return new ConfigurationRedisDto()
            {
                Name = name,
                Type = PropertyType.ToPropertyType(type),
                Value = value,
                ApplicationName = applicationName
            };
        }
    }
}
