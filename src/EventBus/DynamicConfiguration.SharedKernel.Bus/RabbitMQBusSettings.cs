using System;

namespace DynamicConfiguration.SharedKernel.Bus
{
    public class RabbitMQBusSettings
    {
        public string Scheme { get; set; }
        public string Host { get; set; }
        public string VirtualHost { get; set; }
        public int? Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Uri GetUri() => new Uri(getHostAddress());

        private string getHostAddress()
        {
            string hostAddress = default;
            if (!string.IsNullOrWhiteSpace(Scheme))
                hostAddress = $"{Scheme}://";

            hostAddress = $"{hostAddress}{Host}";

            if (Port.HasValue)
                hostAddress = $"{hostAddress}:{Port}";
            if (!string.IsNullOrWhiteSpace(VirtualHost) && VirtualHost != "/")
                hostAddress = $"{hostAddress}/{VirtualHost}";

            return hostAddress;
        }
    }
}