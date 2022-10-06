using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DynamicConfiguration.Application.Configurations.Dtos;
using DynamicConfiguration.Domain.Configurations.Ports;
using DynamicConfiguration.Core.Application.Abstractions.Mediator;
using DynamicConfiguration.Core.Application.Handlers;
using DynamicConfiguration.Core.Application.Models;

namespace DynamicConfiguration.Application.Configurations.Queries.GetListConfigurations
{
    public class GetListConfigurationsQueryHandler : AppResponseHandler<List<ConfigurationDto>>, IQueryHandler<GetListConfigurationsQuery, List<ConfigurationDto>>
    {
        private readonly IConfigurationDataPort _configurationDataPort;

        public GetListConfigurationsQueryHandler(IConfigurationDataPort configurationDataPort)
        {
            _configurationDataPort = configurationDataPort;
        }

        public async Task<AppResponse<List<ConfigurationDto>>> Handle(GetListConfigurationsQuery request, CancellationToken cancellationToken)
        {
            var configurations = await _configurationDataPort.ListAsync();
            var configurationDtos = configurations.Select(ConfigurationDto.Map).ToList();
            return OK(configurationDtos);
        }
    }
}