using System;
using DynamicConfiguration.SharedKernel.Bus;
using MassTransit;

namespace DynamicConfiguration.Bus
{
    public interface IDynamicConfigurationBus : IBus { }
    public interface IDynamicConfigurationBusPublisher : IBusPublisher { }
    internal class DynamicConfigurationBus : BusPublisher<IDynamicConfigurationBus>, IDynamicConfigurationBusPublisher
    {
        public DynamicConfigurationBus(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}