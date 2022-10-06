using System.Threading.Tasks;
using DynamicConfiguration.Application.Configurations.Commands.CreateConfiguration;
using DynamicConfiguration.Application.Configurations.Commands.DeleteConfiguration;
using DynamicConfiguration.Application.Configurations.Commands.UpdateConfiguration;
using DynamicConfiguration.Application.Configurations.Dtos;
using DynamicConfiguration.Application.Configurations.Queries.GetConfiguration;
using DynamicConfiguration.Application.Configurations.Queries.GetListConfigurations;
using DynamicConfiguration.WEB.Common.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DynamicConfiguration.WEB.Controllers
{
    public class ConfigurationController : DynamicConfigurationController
    {
        [HttpGet("list")]
        public async Task<IActionResult> Index()
        {
            var response = await _mediator.Send(new GetListConfigurationsQuery());
            return View(response);
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Detail([FromRoute] string id)
        {
            if(string.IsNullOrWhiteSpace(id))
                return View(new ConfigurationDto());
            var response = await _mediator.Send(new GetConfigurationQuery(id));
            return View(response.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync([FromForm] CreateConfigurationCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateConfigurationCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromForm] DeleteConfigurationCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
