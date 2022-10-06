using DynamicConfiguration.Core.Application.Abstractions.Mediator;

namespace DynamicConfiguration.Application.Configurations.Queries.GetConfigurationFromRedis
{
    public class GetConfigurationFromRedisQuery
    {
        public GetConfigurationFromRedisQuery(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
    public class GetConfigurationStringFromRedisQuery : GetConfigurationFromRedisQuery, IQuery<string>
    {
        public GetConfigurationStringFromRedisQuery(string name) : base(name)
        {
            
        }
    }
    public class GetConfigurationIntFromRedisQuery : GetConfigurationFromRedisQuery, IQuery<string>
    {
        public GetConfigurationIntFromRedisQuery(string name) : base(name)
        {
            
        }
    }
    public class GetConfigurationBoolFromRedisQuery : GetConfigurationFromRedisQuery, IQuery<string>
    {
        public GetConfigurationBoolFromRedisQuery(string name) : base(name)
        {
            
        }
    }
    public class GetConfigurationDoubleFromRedisQuery : GetConfigurationFromRedisQuery, IQuery<string>
    {
        public GetConfigurationDoubleFromRedisQuery(string name) : base(name)
        {
            
        }
    }
}
