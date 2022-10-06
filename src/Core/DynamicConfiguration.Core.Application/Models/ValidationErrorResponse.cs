using System.Collections.Generic;

namespace DynamicConfiguration.Core.Application.Models
{
    public class ValidationErrorResponse
    {
        public ValidationErrorResponse() { }
        public ValidationErrorResponse(string field, IEnumerable<string> messages)
        {
            this.Field = field;
            this.Messages = messages;
        }
        public string Field { get; set; }
        public IEnumerable<string> Messages { get; set; }
    }
}