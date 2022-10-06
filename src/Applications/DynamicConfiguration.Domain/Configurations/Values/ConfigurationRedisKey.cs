using System;
using DynamicConfiguration.Core.Domain;

namespace DynamicConfiguration.Domain.Configurations.Values
{
    public sealed partial record ConfigurationRedisKey : _ImmutableValue<string>
    {
        public ConfigurationRedisKey(string key) : base(key) { }
        public string RedisKey => toRedisKey(this);
    }

    public sealed partial record ConfigurationRedisKey
    {
        private const string DYNAMIC_CONFIGURATION_API_KEY = "DynamicConfiguration.API";
        private const string DYNAMIC_CONFIGURATION_WEB_KEY = "DynamicConfiguration.WEB";
        private const string SERVICE_A_KEY = "DynamicConfiguration.ServiceA";
        private const string SERVICE_B_KEY = "DynamicConfiguration.ServiceB";

        public static readonly ConfigurationRedisKey DynamicConfigurationApiKey = new ConfigurationRedisKey(DYNAMIC_CONFIGURATION_API_KEY);
        public static readonly ConfigurationRedisKey DynamicConfigurationWebKey = new ConfigurationRedisKey(DYNAMIC_CONFIGURATION_WEB_KEY);
        public static readonly ConfigurationRedisKey ServiceAKey = new ConfigurationRedisKey(SERVICE_A_KEY);
        public static readonly ConfigurationRedisKey ServiceBKey = new ConfigurationRedisKey(SERVICE_B_KEY);

        private string toRedisKey(ConfigurationRedisKey type) => type.Key switch
        {
            DYNAMIC_CONFIGURATION_API_KEY => "DYNAMIC_CONFIGURATION_API",
            DYNAMIC_CONFIGURATION_WEB_KEY => "DYNAMIC_CONFIGURATION_WEB",
            SERVICE_A_KEY => "SERVICE_A",
            SERVICE_B_KEY => "SERVICE_B",
            _ => throw new ArgumentOutOfRangeException(nameof(PropertyType), $"Not expected direction value: {type.Key}"),
        };

        public static ConfigurationRedisKey ToPropertyType(string key) => key switch
        {
            DYNAMIC_CONFIGURATION_API_KEY => DynamicConfigurationApiKey,
            DYNAMIC_CONFIGURATION_WEB_KEY => DynamicConfigurationWebKey,
            SERVICE_A_KEY => ServiceAKey,
            SERVICE_B_KEY => ServiceBKey,
            _ => throw new ArgumentOutOfRangeException(nameof(ConfigurationRedisKey), $"Not expected direction value: {key}"),
        };
    }
}