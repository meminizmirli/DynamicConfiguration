using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicConfiguration.Core.Application
{
    public static class StartupSetup
    {
        public static IServiceCollection AddDynamicConfigurationCoreApplication(this IServiceCollection services, Assembly mediatrAssembly)
        {
            services.AddMediatR(mediatrAssembly);
            return services;
        }
    }
}