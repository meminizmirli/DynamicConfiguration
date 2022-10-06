using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicConfiguration.Domain.Caches.Ports
{
    public interface ICacheEventBusPort
    {
        Task CacheClearAsync(string redisKey);
    }
}
