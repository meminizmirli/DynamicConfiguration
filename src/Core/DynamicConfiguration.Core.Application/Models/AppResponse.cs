using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace DynamicConfiguration.Core.Application.Models
{
    public class AppResponse<T> : AppBaseResponse
    {
        public AppResponse() { }
        public AppResponse(int statusCode, bool success, string exceptionType, IEnumerable<object> errors) : base(statusCode, success, exceptionType, errors) { }
        public AppResponse(int statusCode, bool success, string exceptionType, IEnumerable<object> errors, T data) : base(statusCode, success, exceptionType, errors) => this.Data = data;
        public AppResponse(int statusCode, bool success, string exceptionId, string exceptionType, IEnumerable<object> errors) : base(statusCode, success, exceptionId, exceptionType, errors)
        {

        }
        public AppResponse(T data) : base(StatusCodes.Status200OK, true) => this.Data = data;
      
        public T Data { get; set; }

        #region Methods
        public static AppResponse<T> OK(T data = default(T)) => new AppResponse<T>(data);
        #endregion
    }
}