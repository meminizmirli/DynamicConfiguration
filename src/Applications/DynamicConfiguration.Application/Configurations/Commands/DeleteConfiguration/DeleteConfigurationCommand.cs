using DynamicConfiguration.Core.Application.Abstractions.Mediator;
using DynamicConfiguration.Core.Application.Constants;
using FluentValidation;

namespace DynamicConfiguration.Application.Configurations.Commands.DeleteConfiguration
{
    public class DeleteConfigurationCommand : ICommand<bool>
    {
        public string Id { get; set; }
    }

    #region Validator
    public class DeleteConfigurationValidator : AbstractValidator<DeleteConfigurationCommand>
    {
        public DeleteConfigurationValidator()
        {
            RuleFor(q => q.Id)
                .NotNull().WithMessage($"{NameOfEntities.Configuration} Id boş olamaz")
                .NotEmpty().WithMessage($"{NameOfEntities.Configuration} Id boş olamaz");
        }
    }
    #endregion
}
