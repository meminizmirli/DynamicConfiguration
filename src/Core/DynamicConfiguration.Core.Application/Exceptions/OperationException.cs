namespace DynamicConfiguration.Core.Application.Exceptions
{
    public class OperationException : ApplicationCoreException
    {
        private const string ID = "OPR-101";
        private const string MESSAGE = "Bir hata ile karşılaştık, lütfen işlemi tekrar deneyin. Sorunuz devam ederse lütfen müşteri hizmetleri ile iletişime geçin.";
        public OperationException(string message) : base("NO-ID", message)
        {

        }
        public OperationException(string id = ID, string message = MESSAGE) : base(id, message)
        {
        }
    }
}