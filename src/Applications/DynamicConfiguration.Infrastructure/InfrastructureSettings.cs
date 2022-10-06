using System;

namespace DynamicConfiguration.Infrastructure
{
    public class InfrastructureSettings
    {
        public string ApplicationName { get; set; }
        public TimeSpan RefreshTimerIntervalInMs { get; set; }
    }
}