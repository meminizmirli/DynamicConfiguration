using System.Threading;
using System.Threading.Tasks;
using DynamicConfiguration.Application.Configurations.Dtos;
using DynamicConfiguration.Domain.Configurations;
using DynamicConfiguration.Domain.Configurations.Ports;
using DynamicConfiguration.Core.Application.Abstractions.Mediator;
using DynamicConfiguration.Core.Application.Exceptions.Base;
using DynamicConfiguration.Core.Application.Handlers;
using DynamicConfiguration.Core.Application.Models;

namespace DynamicConfiguration.Application.Configurations.Commands.CreateConfiguration
{
    public class CreateConfigurationCommandHandler : AppResponseHandler<ConfigurationDto>, ICommandHandler<CreateConfigurationCommand, ConfigurationDto>
    {
        private readonly IConfigurationDataPort _configurationDataPort;
        private readonly IConfigurationRedisDataPort _configurationRedisDataPort;

        public CreateConfigurationCommandHandler(IConfigurationDataPort configurationDataPort, IConfigurationRedisDataPort configurationRedisDataPort)
        {
            _configurationDataPort = configurationDataPort;
            _configurationRedisDataPort = configurationRedisDataPort;
        }

        public async Task<AppResponse<ConfigurationDto>> Handle(CreateConfigurationCommand request, CancellationToken cancellationToken)
        {
            var configuration = Configuration.Create(
                name: request.Name,
                type: request.Type,
                value: request.Value,
                applicationName: request.ApplicationName);
            (await _configurationDataPort.CreateAsync(configuration))
                .EnsureSuccessOperation();

            await _configurationRedisDataPort.ClearCacheAsync();

            var configurationDto = ConfigurationDto.Map(configuration);
            return OK(configurationDto);
        }
    }
}