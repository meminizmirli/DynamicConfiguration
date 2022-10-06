using DynamicConfiguration.Core.Application.Models;
using MediatR;

namespace DynamicConfiguration.Core.Application.Abstractions.Mediator
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, AppResponse<TResponse>> where TQuery : IQuery<TResponse>
    {

    }
}