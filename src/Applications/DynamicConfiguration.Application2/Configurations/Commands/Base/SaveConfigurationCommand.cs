using DynamicConfiguration.Domain.Configurations.Values;
using DynamicConfiguration.Core.Application.Constants;
using DynamicConfiguration.Core.Application.Exceptions.Common;
using FluentValidation;

namespace DynamicConfiguration.Application.Configurations.Commands.Base
{
    public class SaveConfigurationCommand
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string ApplicationName { get; set; }
    }

    #region Validator
    public class SaveConfigurationValidator<T> : AbstractValidator<T> where T : SaveConfigurationCommand
    {
        public SaveConfigurationValidator()
        {
            RuleFor(q => q.Name)
                .MaximumLength(200).WithMessage($"{NameOfEntities.Configuration} adı maksimum 200 karakter olabilir.")
                .NotNull().WithMessage($"{NameOfEntities.Configuration} adı boş olamaz")
                .NotEmpty().WithMessage($"{NameOfEntities.Configuration} adı boş olamaz");
            RuleFor(q => q.Value)
                .MaximumLength(200).WithMessage($"{NameOfEntities.Configuration} değeri maksimum 200 karakter olabilir.")
                .NotNull().WithMessage($"{NameOfEntities.Configuration} değeri boş olamaz")
                .NotEmpty().WithMessage($"{NameOfEntities.Configuration} değeri boş olamaz");
            RuleFor(q => q.ApplicationName)
                .MaximumLength(200).WithMessage($"{NameOfEntities.Configuration} uygulama adı maksimum 200 karakter olabilir.")
                .NotNull().WithMessage($"{NameOfEntities.Configuration} uygulama adı boş olamaz")
                .NotEmpty().WithMessage($"{NameOfEntities.Configuration} uygulama adı boş olamaz");

            RuleFor(q => q.Type)
                .Custom((q, context) =>
                {
                    try
                    {
                        var propertyType = PropertyType.ToPropertyType(q);
                    }
                    catch
                    {
                        throw new NotFoundException(message: $"{NameOfEntities.Configuration} tipi kabul edilen tiplerden değil.");
                    }
                });
        }
    }
    #endregion
}
