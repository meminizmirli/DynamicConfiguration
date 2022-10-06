using System;

namespace Redis.Hub.Extensions
{
    internal static class DateTimeExtension
    {
        public static TimeSpan ToTimeSpan(this DateTime dateTime)
        {
            var timeSpanExpiration = dateTime - DateTime.Now;
            timeSpanExpiration = TimeSpan.FromMilliseconds(timeSpanExpiration.TotalMilliseconds);

            return timeSpanExpiration;
        }
    }
}