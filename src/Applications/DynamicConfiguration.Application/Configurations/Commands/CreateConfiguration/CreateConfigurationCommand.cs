using DynamicConfiguration.Application.Configurations.Commands.Base;
using DynamicConfiguration.Application.Configurations.Dtos;
using DynamicConfiguration.Core.Application.Abstractions.Mediator;

namespace DynamicConfiguration.Application.Configurations.Commands.CreateConfiguration
{
    public class CreateConfigurationCommand : SaveConfigurationCommand, ICommand<ConfigurationDto>
    {
    }

    #region Validator
    public class CreateConfigurationValidator : SaveConfigurationValidator<CreateConfigurationCommand>
    {
        public CreateConfigurationValidator()
        {
        }
    }
    #endregion
}
