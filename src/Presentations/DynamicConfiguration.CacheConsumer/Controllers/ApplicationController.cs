using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DynamicConfiguration.CacheConsumer.Controllers

{
    public class ApplicationController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ApplicationController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet("environment-variables")]
        public async Task<IActionResult> EnvironmentVariablesAsync()
        {
            var environmentVariables = new
            {
                environment = _webHostEnvironment.EnvironmentName
            };

            return await Task.FromResult(Ok(environmentVariables));
        }
    }
}