using System;

namespace DynamicConfiguration.Core.Domain.Exceptions
{
    public class DynamicConfigurationExceptionBase : Exception
    {
        public string EXCEPTION_ID { get; protected set; }
        public DynamicConfigurationExceptionBase(string id, string message) : base(message)
        {
            EXCEPTION_ID = id;
        }
    }
}