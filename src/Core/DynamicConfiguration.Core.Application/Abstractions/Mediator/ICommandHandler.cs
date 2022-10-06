using DynamicConfiguration.Core.Application.Models;
using MediatR;

namespace DynamicConfiguration.Core.Application.Abstractions.Mediator
{
    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, AppResponse<TResponse>> where TCommand : ICommand<TResponse>
    {
    }

}