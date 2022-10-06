using System.Threading;
using System.Threading.Tasks;
using DynamicConfiguration.Application.Configurations.Dtos;
using DynamicConfiguration.Application.Configurations.Exceptions;
using DynamicConfiguration.Domain.Configurations.Ports;
using DynamicConfiguration.Core.Application.Abstractions.Mediator;
using DynamicConfiguration.Core.Application.Exceptions.Base;
using DynamicConfiguration.Core.Application.Handlers;
using DynamicConfiguration.Core.Application.Models;

namespace DynamicConfiguration.ConfigurationModule.Application.Configurations.Commands.UpdateConfiguration
{
    public class UpdateConfigurationCommandHandler : AppResponseHandler<ConfigurationDto>, ICommandHandler<UpdateConfigurationCommand, ConfigurationDto>
    {
        private readonly IConfigurationDataPort _configurationDataPort;

        public UpdateConfigurationCommandHandler(IConfigurationDataPort configurationDataPort)
        {
            _configurationDataPort = configurationDataPort;
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

            var configurationDto = ConfigurationDto.Map(configuration);
            return OK(configurationDto);
        }
    }
}