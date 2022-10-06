using DynamicConfiguration.Bus;
using DynamicConfiguration.SharedKernel.Bus;
using MassTransit;
using MassTransit.MultiBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicConfiguration.Infrastructure.EventBusMassTransit
{
    public static class StartupSetup
    {
        private static readonly RabbitMQBusSettings _rabbiMQBusSettings = new RabbitMQBusSettings();
        public static IServiceCollection AddEventBusMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            configuration.Bind(nameof(RabbitMQBusSettings), _rabbiMQBusSettings);
            services.AddDynamicConfigurationBusAsPublisher(configuration);
            services.AddMassTransitHostedService(true);

            return services;
        }

        private static IServiceCollection AddDynamicConfigurationBusAsPublisher(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDynamicConfigurationBus()
                    .AddMassTransit<IDynamicConfigurationBus>(x =>
                    {
                        x.SetKebabCaseEndpointNameFormatter();
                        x.UsingRabbitMq((context, configuration) =>
                        {
                            configuration.ConfigureEndpoints(context);
                            configuration.Host(_rabbiMQBusSettings.GetUri(), h =>
                            {
                                h.Username(_rabbiMQBusSettings.Username);
                                h.Password(_rabbiMQBusSettings.Password);
                            });
                        });
                    });

            return services;
        }
    }

}