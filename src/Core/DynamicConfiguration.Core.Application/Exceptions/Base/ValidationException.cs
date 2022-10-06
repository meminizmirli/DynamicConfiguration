using System;
using System.Collections.Generic;
using DynamicConfiguration.Core.Application.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace DynamicConfiguration.Core.Application.Exceptions.Base
{
    public class ValidationException : Exception
    {
        public ValidationException(List<ValidationErrorResponse> validationErrorResponse)
        {
            var validationFailures = new List<ValidationFailure>();
            foreach (var item in validationErrorResponse)
                foreach (var message in item.Messages)
                    validationFailures.Add(new ValidationFailure(item.Field, message));

            HResult = StatusCodes.Status422UnprocessableEntity;
            Data.Add("validationFailuresResult", validationFailures);
        }

    }
}