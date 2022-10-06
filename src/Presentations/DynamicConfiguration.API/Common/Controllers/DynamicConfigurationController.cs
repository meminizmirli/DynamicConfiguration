using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicConfiguration.API.Common.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DynamicConfigurationController : ControllerBase
    {
        protected IMediator Mediator;
        protected IMediator _mediator => Mediator ?? HttpContext.RequestServices.GetService<IMediator>();
    }
}
