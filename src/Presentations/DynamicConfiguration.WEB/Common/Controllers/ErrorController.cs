using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DynamicConfiguration.Core.Application.Exceptions.Common;
using DynamicConfiguration.Core.Application.Extensions;
using DynamicConfiguration.Core.Application.Models;
using DynamicConfiguration.Core.Domain.Constants;
using DynamicConfiguration.Core.Domain.Exceptions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DynamicConfiguration.WEB.Common.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : DynamicConfigurationController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ErrorController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet, HttpPost, HttpPut, HttpDelete]
        public async Task<IActionResult> OutputAsync()
        {
            var exceptionHandler = await Task.FromResult(_httpContextAccessor.HttpContext.Features.Get<IExceptionHandlerPathFeature>());
            var exception = exceptionHandler.Error;
            if (exception.HResult == StatusCodes.Status422UnprocessableEntity)
            {
                var validationFailures = (List<ValidationFailure>)exception.Data["validationFailuresResult"];
                var validationErrors = validationFailures.GroupBy(v => new { v.PropertyName })
                                                         .Select(q => new ValidationErrorResponse(field: q.Key.PropertyName.ToValidationField(),
                                                                                                  messages: q.Select(x => x.ErrorMessage)));

                var validationErrorResponseDto = new AppResponse<object>(statusCode: StatusCodes.Status422UnprocessableEntity,
                                                                         success: false,
                                                                         exceptionType: ExceptionTypes.Validation,
                                                                        errors: validationErrors);

                return new UnprocessableEntityObjectResult(validationErrorResponseDto);
            }

            if (exception is DynamicConfigurationExceptionBase @base)
            {
                var responseDto = new AppResponse<object>(
                   statusCode: StatusCodes.Status500InternalServerError,
                   success: false,
                   exceptionId: @base.EXCEPTION_ID,
                   exceptionType: ExceptionTypes.Application,
                   errors: new string[] { @base.Message }
               );

                if (exception is NotFoundException)
                    return new BadRequestObjectResult(responseDto);

                return new ObjectResult(responseDto) { StatusCode = StatusCodes.Status500InternalServerError };
            }
            else
            {
                var responseDto = new AppResponse<object>(statusCode: StatusCodes.Status500InternalServerError,
                                                          success: false,
                                                          exceptionType: ExceptionTypes.System,
                                                          errors: new string[] { exception.Message });

                return new ObjectResult(responseDto) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
    }
}