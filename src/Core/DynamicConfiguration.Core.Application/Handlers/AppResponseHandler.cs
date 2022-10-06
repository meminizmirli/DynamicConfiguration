using DynamicConfiguration.Core.Application.Models;

namespace DynamicConfiguration.Core.Application.Handlers
{
    public class AppResponseHandler<TResponse>
    {
        public AppResponse<bool> OK() => new AppResponse<bool>(true);
        public AppResponse<TResponse> OK(TResponse data) => new AppResponse<TResponse>(data);

    }
}