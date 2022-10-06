using DynamicConfiguration.Application.Configurations.Commands.Base;
using DynamicConfiguration.Application.Configurations.Commands.CreateConfiguration;
using DynamicConfiguration.Application.Configurations.Commands.DeleteConfiguration;
using DynamicConfiguration.Application.Configurations.Commands.UpdateConfiguration;
using DynamicConfiguration.Core.Application;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicConfiguration.Application
{
    public static class StartupSetup
    {
        public static IServiceCollection AddDynamicConfigurationConfigurationModuleApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDynamicConfigurationCoreApplication(mediatrAssembly: typeof(StartupSetup).Assembly);
            services.addApplicationValidation();
            return services;
        }

        private static void addApplicationValidation(this IServiceCollection services)
        {
            #region Configuration
            services.AddTransient<IValidator<SaveConfigurationCommand>, SaveConfigurationValidator<SaveConfigurationCommand>>();
            services.AddTransient<IValidator<CreateConfigurationCommand>, CreateConfigurationValidator>();
            services.AddTransient<IValidator<UpdateConfigurationCommand>, UpdateConfigurationValidator>();
            services.AddTransient<IValidator<DeleteConfigurationCommand>, DeleteConfigurationValidator>();
            #endregion

        }
    }
}
