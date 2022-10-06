using DynamicConfiguration.Core.Application.Models;
using MediatR;

namespace DynamicConfiguration.Core.Application.Abstractions.Mediator
{
    public interface ICommand<TResponse> : IRequest<AppResponse<TResponse>>
    {

    }
}