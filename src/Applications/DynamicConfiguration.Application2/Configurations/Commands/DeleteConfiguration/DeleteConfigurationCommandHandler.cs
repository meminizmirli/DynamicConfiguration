using System.Threading;
using System.Threading.Tasks;
using DynamicConfiguration.Application.Configurations.Exceptions;
using DynamicConfiguration.Domain.Configurations.Ports;
using DynamicConfiguration.Core.Application.Abstractions.Mediator;
using DynamicConfiguration.Core.Application.Exceptions.Base;
using DynamicConfiguration.Core.Application.Handlers;
using DynamicConfiguration.Core.Application.Models;

namespace DynamicConfiguration.Application.Configurations.Commands.DeleteConfiguration
{
    public class DeleteConfigurationCommandHandler : AppResponseHandler<bool>, ICommandHandler<DeleteConfigurationCommand, bool>
    {
        private readonly IConfigurationDataPort _configurationDataPort;

        public DeleteConfigurationCommandHandler(IConfigurationDataPort configurationDataPort)
        {
            _configurationDataPort = configurationDataPort;
        }

        public async Task<AppResponse<bool>> Handle(DeleteConfigurationCommand request, CancellationToken cancellationToken)
        {
            var configuration = await _configurationDataPort.GetByIdAsync(request.Id);
            if (configuration == null)
                throw new ConfigurationNotFoundException();

            configuration.MarkDeleted();
            (await _configurationDataPort.RemoveAsync(configuration))
                .EnsureSuccessOperation();

            return OK();
        }
    }
}