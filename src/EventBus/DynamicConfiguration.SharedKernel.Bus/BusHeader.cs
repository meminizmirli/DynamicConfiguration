using System.Collections.Generic;

namespace DynamicConfiguration.SharedKernel.Bus
{
    public class BusHeader : Dictionary<string, string>
    {
        public const string RedisKey = "Redis-Key";
    }
}