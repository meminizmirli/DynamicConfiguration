using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicConfiguration.WEB.Common.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DynamicConfigurationController : Controller
    {
        protected IMediator Mediator;
        protected IMediator _mediator => Mediator ?? HttpContext.RequestServices.GetService<IMediator>();
    }
}
