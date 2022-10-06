using DynamicConfiguration.Core.Application.Models;
using MediatR;

namespace DynamicConfiguration.Core.Application.Abstractions.Mediator
{
    public interface IQuery<TResponse> : IRequest<AppResponse<TResponse>>
    {

    }
}