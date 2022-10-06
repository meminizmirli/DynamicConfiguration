using System.Threading;
using System.Threading.Tasks;
using DynamicConfiguration.Application.Configurations.Dtos;
using DynamicConfiguration.Application.Configurations.Exceptions;
using DynamicConfiguration.Core.Application.Abstractions.Mediator;
using DynamicConfiguration.Core.Application.Exceptions.Base;
using DynamicConfiguration.Core.Application.Handlers;
using DynamicConfiguration.Core.Application.Models;
using DynamicConfiguration.Domain.Configurations.Ports;

namespace DynamicConfiguration.Application.Configurations.Commands.UpdateConfiguration
{
    public class UpdateConfigurationCommandHandler : AppResponseHandler<ConfigurationDto>, ICommandHandler<UpdateConfigurationCommand, ConfigurationDto>
    {
        private readonly IConfigurationDataPort _configurationDataPort;
        private readonly IConfigurationRedisDataPort _configurationRedisDataPort;

        public UpdateConfigurationCommandHandler(IConfigurationDataPort configurationDataPort, IConfigurationRedisDataPort configurationRedisDataPort)
        {
            _configurationDataPort = configurationDataPort;
            _configurationRedisDataPort = configurationRedisDataPort;
        }

        public async Task<AppResponse<ConfigurationDto>> Handle(UpdateConfigurationCommand request, CancellationToken cancellationToken)
        {
            var configuration = await _configurationDataPort.GetByIdAsync(request.Id);
            if (configuration == null)
                throw new ConfigurationNotFoundException();

            configuration.SetName(name: request.Name);
            configuration.SetType(type: request.Type);
            configuration.SetValue(value: request.Value);
            configuration.SetApplicationName(applicationName: request.ApplicationName);
            configuration.SetStatus(status: request.Status);
            configuration.MarkUpdated();

            (await _configurationDataPort.UpdateAsync(configuration))
                .EnsureSuccessOperation();

            await _configurationRedisDataPort.ClearCacheAsync();

            var configurationDto = ConfigurationDto.Map(configuration);
            return OK(configurationDto);
        }
    }
}