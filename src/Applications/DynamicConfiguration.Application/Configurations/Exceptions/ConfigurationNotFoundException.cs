using DynamicConfiguration.Core.Application.Exceptions.Common;

namespace DynamicConfiguration.Application.Configurations.Exceptions
{
    public class ConfigurationNotFoundException : NotFoundException
    {
        private const string ID = "CFG-404";
        private const string MESSAGE = "Konfigürasyon bulunamadı.";

        internal ConfigurationNotFoundException() : base(id: ID, message: MESSAGE)
        {
        }
    }
}
