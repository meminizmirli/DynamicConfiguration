using DynamicConfiguration.Domain.Configurations.Ports;
using Moq;
using MoqAssist.Core.Dictionary;

namespace DynamicConfiguration.UnitTests.Base
{
    public class DefaultMockDictionary : MoqAssistDictionary
    {
        public override void RegisterMocks()
        {
            Register(new Mock<IConfigurationDataPort>());
            Register(new Mock<IConfigurationRedisDataPort>());
        }
    }
}