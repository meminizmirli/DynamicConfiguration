using System;
using System.Collections.Generic;
using System.Linq;
using DynamicConfiguration.Core.Application.Models;
using DynamicConfiguration.Core.Domain.Constants;

namespace DynamicConfiguration.Core.Application.Exceptions.Base
{
    public static class ExceptionDetector
    {
        public static void EnsureSuccessOperation(this bool result)
        {
            if (!result)
                throw new OperationException();
        }

        public static void ThrowIfRejected(this bool result, string message, int statusCode = 500)
        {
            if (!result)
                throw new OperationException(statusCode.ToString(), message);
        }

        public static void ThrowIfRejected<T>(this AppResponse<T> response)
        {
            if (!response.Success)
            {
                switch (response.ExceptionType)
                {
                    case ExceptionTypes.System:
                        throw new OperationException(message: response.Errors.First().ToString());
                    case ExceptionTypes.Application:
                        throw new OperationException(response.StatusCode.ToString(), response.Errors.First().ToString());
                    case ExceptionTypes.Validation:
                        throw new ValidationException((List<ValidationErrorResponse>)response.Errors);
                    default:
                        throw new Exception("Cannot detect exception from response");
                }
            }
        }

    }
}