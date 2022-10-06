using DynamicConfiguration.Core.Domain.Exceptions;

namespace DynamicConfiguration.Core.Application.Exceptions
{
    public class ApplicationCoreException : DynamicConfigurationExceptionBase
    {
        protected ApplicationCoreException(string id, string message) : base(id, message)
        {
        }
    }
}