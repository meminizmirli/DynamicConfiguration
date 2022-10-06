using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicConfiguration.API.Common.Controllers;
using DynamicConfiguration.Application.Configurations.Commands.CreateConfiguration;
using DynamicConfiguration.Application.Configurations.Commands.DeleteConfiguration;
using DynamicConfiguration.Application.Configurations.Commands.UpdateConfiguration;
using DynamicConfiguration.Application.Configurations.Dtos;
using DynamicConfiguration.Application.Configurations.Events.CacheClearConfiguration;
using DynamicConfiguration.Application.Configurations.Queries.GetConfiguration;
using DynamicConfiguration.Application.Configurations.Queries.GetConfigurationFromRedis;
using DynamicConfiguration.Application.Configurations.Queries.GetListConfigurations;
using DynamicConfiguration.Core.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace DynamicConfiguration.API.Controllers
{
    public class ConfigurationController : DynamicConfigurationController
    {
        private readonly ILogger<ConfigurationController> _logger;

        public ConfigurationController(ILogger<ConfigurationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AppResponse<List<ConfigurationDto>>))]
        public async Task<IActionResult> ListAsync()
        {
            return Ok(await _mediator.Send(new GetListConfigurationsQuery()));
        }

        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AppResponse<ConfigurationDto>))]
        public async Task<IActionResult> GetAsync([FromRoute] string id)
        {
            return Ok(await _mediator.Send(new GetConfigurationQuery(id)));
        }

        [HttpGet("string/{name}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AppResponse<string>))]
        public async Task<IActionResult> GetStringValueAsync([FromRoute] string name)
        {
            return Ok(await _mediator.Send(new GetConfigurationStringFromRedisQuery(name)));
        }

        [HttpGet("int/{name}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AppResponse<string>))]
        public async Task<IActionResult> GetIntValueAsync([FromRoute] string name)
        {
            return Ok(await _mediator.Send(new GetConfigurationIntFromRedisQuery(name)));
        }

        [HttpGet("boolean/{name}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AppResponse<string>))]
        public async Task<IActionResult> GetBoolValueAsync([FromRoute] string name)
        {
            return Ok(await _mediator.Send(new GetConfigurationBoolFromRedisQuery(name)));
        }

        [HttpGet("double/{name}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AppResponse<string>))]
        public async Task<IActionResult> GetDoubleValueAsync([FromRoute] string name)
        {
            return Ok(await _mediator.Send(new GetConfigurationDoubleFromRedisQuery(name)));
        }

        [HttpPost("cacheclear")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AppResponse<ConfigurationDto>))]
        public async Task<IActionResult> CacheClearAsync([FromBody] CacheClearConfigurationEvent @event)
        {
            await _mediator.Publish(@event);
            return Ok();
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AppResponse<ConfigurationDto>))]
        public async Task<IActionResult> CreateAsync([FromBody] CreateConfigurationCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AppResponse<ConfigurationDto>))]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateConfigurationCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AppResponse<bool>))]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteConfigurationCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
