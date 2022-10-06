using MediatR;

namespace DynamicConfiguration.Core.Application.Abstractions.Mediator
{
    public interface IEventHandler<TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
    {
    }
}