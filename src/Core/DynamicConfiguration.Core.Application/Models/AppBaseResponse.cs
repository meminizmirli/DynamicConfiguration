using System.Collections.Generic;

namespace DynamicConfiguration.Core.Application.Models
{
    public class AppBaseResponse
    {
        public AppBaseResponse()
        {

        }
        public AppBaseResponse(int statusCode, bool success, string exceptionType, IEnumerable<object> errors)
        {
            this.StatusCode = statusCode;
            this.Success = success;
            this.ExceptionType = exceptionType;
            this.Errors = errors;
        }

        public AppBaseResponse(int statusCode, bool success, string exceptionId, string exceptionType, IEnumerable<object> errors)
        {
            this.StatusCode = statusCode;
            this.Success = success;
            this.ExceptionId = exceptionId;
            this.ExceptionType = exceptionType;
            this.Errors = errors;
        }
        public AppBaseResponse(int statusCode, bool success)
        {
            this.StatusCode = statusCode;
            this.Success = success;
        }
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string ExceptionId { get; set; }
        public string ExceptionType { get; set; }
        public IEnumerable<object> Errors { get; set; }

    }
}