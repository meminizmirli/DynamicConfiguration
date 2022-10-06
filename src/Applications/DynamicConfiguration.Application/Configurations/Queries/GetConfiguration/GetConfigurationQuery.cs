using DynamicConfiguration.Application.Configurations.Dtos;
using DynamicConfiguration.Core.Application.Abstractions.Mediator;

namespace DynamicConfiguration.Application.Configurations.Queries.GetConfiguration
{
    public class GetConfigurationQuery : IQuery<ConfigurationDto>
    {
        public GetConfigurationQuery(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
    }
}
