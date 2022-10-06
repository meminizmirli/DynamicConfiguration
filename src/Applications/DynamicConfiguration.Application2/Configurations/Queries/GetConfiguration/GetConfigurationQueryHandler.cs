using System.Threading;
using System.Threading.Tasks;
using DynamicConfiguration.Application.Configurations.Dtos;
using DynamicConfiguration.Application.Configurations.Exceptions;
using DynamicConfiguration.Domain.Configurations.Ports;
using DynamicConfiguration.Core.Application.Abstractions.Mediator;
using DynamicConfiguration.Core.Application.Handlers;
using DynamicConfiguration.Core.Application.Models;

namespace DynamicConfiguration.Application.Configurations.Queries.GetConfiguration
{
    public class GetConfigurationQueryHandler : AppResponseHandler<ConfigurationDto>, IQueryHandler<GetConfigurationQuery, ConfigurationDto>
    {
        private readonly IConfigurationDataPort _configurationDataPort;

        public GetConfigurationQueryHandler(IConfigurationDataPort configurationDataPort)
        {
            _configurationDataPort = configurationDataPort;
        }

        public async Task<AppResponse<ConfigurationDto>> Handle(GetConfigurationQuery request, CancellationToken cancellationToken)
        {
            var configuration = await _configurationDataPort.GetByIdAsync(request.Id);
            if (configuration == null)
                throw new ConfigurationNotFoundException();

            var configurationDto = ConfigurationDto.Map(configuration);
            return OK(configurationDto);
        }
    }
}