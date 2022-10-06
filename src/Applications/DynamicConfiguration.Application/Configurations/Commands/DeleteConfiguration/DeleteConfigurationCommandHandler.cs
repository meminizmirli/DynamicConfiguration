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
        private readonly IConfigurationRedisDataPort _configurationRedisDataPort;

        public DeleteConfigurationCommandHandler(IConfigurationDataPort configurationDataPort, IConfigurationRedisDataPort configurationRedisDataPort)
        {
            _configurationDataPort = configurationDataPort;
            _configurationRedisDataPort = configurationRedisDataPort;
        }

        public async Task<AppResponse<bool>> Handle(DeleteConfigurationCommand request, CancellationToken cancellationToken)
        {
            var configuration = await _configurationDataPort.GetByIdAsync(request.Id);
            if (configuration == null)
                throw new ConfigurationNotFoundException();

            configuration.MarkDeleted();
            (await _configurationDataPort.RemoveAsync(configuration))
                .EnsureSuccessOperation();

            await _configurationRedisDataPort.ClearCacheAsync();

            return OK();
        }
    }
}