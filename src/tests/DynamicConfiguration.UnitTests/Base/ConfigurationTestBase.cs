using System;
using DynamicConfiguration.Domain.Configurations;

namespace DynamicConfiguration.UnitTests.Base
{
    public class ConfigurationTestBase
    {
        public static Configuration GetGeneratedConfiguration() => Configuration.Map(
            id: "1234567",
            isDeleted: false,
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now,
            status: 1,
            name: "Test",
            type: "String",
            value: "Test",
            applicationName: "DynamicConfiguration.UnitTests");
    }
}
