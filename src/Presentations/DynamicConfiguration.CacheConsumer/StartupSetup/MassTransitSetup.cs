using DynamicConfiguration.Bus;
using DynamicConfiguration.CacheConsumer.Application.Consumers;
using DynamicConfiguration.SharedKernel.Bus;
using MassTransit;
using MassTransit.MultiBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicConfiguration.CacheConsumer.StartupSetup
{
    public static class MassTransitSetup
    {
        private static readonly RabbitMQBusSettings _rabbiMQBusSettings = new RabbitMQBusSettings();
        public static IServiceCollection AddDynamicConfigurationCacheConsumerMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            configuration.Bind(nameof(RabbitMQBusSettings), _rabbiMQBusSettings);
            services.addDynamicConfigurationBusMassTransit();
            services.AddMassTransitHostedService(true);
            return services;
        }

        private static IServiceCollection addDynamicConfigurationBusMassTransit(this IServiceCollection services)
        {
            services.AddDynamicConfigurationBus()
                .AddMassTransit<IDynamicConfigurationBus>(x =>
                {
                    x.AddConsumer<CacheClearConsumer>();
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