using System.Collections.Generic;
using DynamicConfiguration.Application.Configurations.Dtos;
using DynamicConfiguration.Core.Application.Abstractions.Mediator;

namespace DynamicConfiguration.Application.Configurations.Queries.GetListConfigurations
{
    public class GetListConfigurationsQuery : IQuery<List<ConfigurationDto>>
    {
    }
}
