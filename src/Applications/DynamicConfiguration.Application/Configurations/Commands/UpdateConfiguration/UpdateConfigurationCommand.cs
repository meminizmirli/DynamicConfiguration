using DynamicConfiguration.Application.Configurations.Commands.Base;
using DynamicConfiguration.Application.Configurations.Dtos;
using DynamicConfiguration.Core.Application.Abstractions.Mediator;
using DynamicConfiguration.Core.Application.Constants;
using FluentValidation;

namespace DynamicConfiguration.Application.Configurations.Commands.UpdateConfiguration
{
    public class UpdateConfigurationCommand : SaveConfigurationCommand, ICommand<ConfigurationDto>
    {
        public string Id { get; set; }
        public bool Status { get; set; }
    }

    #region Validator
    public class UpdateConfigurationValidator : SaveConfigurationValidator<UpdateConfigurationCommand>
    {
        public UpdateConfigurationValidator()
        {
            RuleFor(q => q.Id)
                .NotNull().WithMessage($"{NameOfEntities.Configuration} Id boş olamaz")
                .NotEmpty().WithMessage($"{NameOfEntities.Configuration} Id boş olamaz");
        }
    }
    #endregion
}
