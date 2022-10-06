using DynamicConfiguration.SharedKernel.Bus.Contracts;

namespace DynamicConfiguration.Bus.Contracts.Commands
{
    public interface ICacheClearCommand : IBusCommand
    {
        public string RedisKey { get; set; }
    }
}