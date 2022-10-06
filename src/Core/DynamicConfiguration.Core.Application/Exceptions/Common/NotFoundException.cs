namespace DynamicConfiguration.Core.Application.Exceptions.Common
{
    public class NotFoundException : ApplicationCoreException
    {
        private const string ID = "NTF-404";
        private const string MESSAGE = "Sonuç bulunamadı.";
        
        public NotFoundException(string id = ID, string message = MESSAGE) : base(id, message)
        {
        }
    }
}