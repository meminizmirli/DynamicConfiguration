using DynamicConfiguration.Domain.Configurations;
using Xunit;

namespace DynamicConfiguration.UnitTests.Domain.MappingTests.Configurations
{
    public class ConfigurationRedisDtoTests
    {
        
        [Fact]
        public void Map_Should_Be_Successful()
        {
            string name = "Test";
            string type = "String";
            string value = "Test";
            string applicationName = "DynamicConfiguration.UnitTests";

            var user = ConfigurationRedisDto.Map(
                name: name,
                type: type,
                value: value,
                applicationName: applicationName);

            Assert.Equal(name, user.Name);
            Assert.Equal(type, user.Type.Key);
            Assert.Equal(value, user.Value);
            Assert.Equal(applicationName, user.ApplicationName);
        }
    }
}
