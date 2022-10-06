using System;
using System.Threading;
using System.Threading.Tasks;
using DynamicConfiguration.SharedKernel.Bus.Contracts;
using GreenPipes;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicConfiguration.SharedKernel.Bus
{
    public interface IBusPublisher
    {
        ConnectHandle ConnectPublishObserver(IPublishObserver observer);
        Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : class, IBusContract;
        Task Publish<T>(T message, IPipe<PublishContext<T>> publishPipe, CancellationToken cancellationToken = default) where T : class, IBusContract;
        Task Publish<T>(T message, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default) where T : class, IBusContract;
    }
    public class BusPublisher<TBus> where TBus : IBus
    {
        private IPublishEndpoint _publishEndpoint;
        public BusPublisher(IServiceProvider serviceProvider) => _publishEndpoint = serviceProvider.GetService<Bind<TBus, IPublishEndpoint>>().Value;
        public ConnectHandle ConnectPublishObserver(IPublishObserver observer) => _publishEndpoint.ConnectPublishObserver(observer);
        public async Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : class, IBusContract
            => await _publishEndpoint.Publish<T>(message, cancellationToken);
        public async Task Publish<T>(T message, IPipe<PublishContext<T>> publishPipe, CancellationToken cancellationToken = default) where T : class, IBusContract
            => await _publishEndpoint.Publish<T>(message, publishPipe, cancellationToken);
        public async Task Publish<T>(T message, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default) where T : class, IBusContract
            => await _publishEndpoint.Publish<T>(message, publishPipe, cancellationToken);
    }
}