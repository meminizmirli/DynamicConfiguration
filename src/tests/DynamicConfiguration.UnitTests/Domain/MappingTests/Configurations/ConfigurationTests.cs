using System;
using DynamicConfiguration.Core.Domain.Constants;
using DynamicConfiguration.Domain.Configurations;
using DynamicConfiguration.Domain.Configurations.Values;
using DynamicConfiguration.UnitTests.Base;
using Xunit;

namespace DynamicConfiguration.UnitTests.Domain.MappingTests.Configurations
{
    public class ConfigurationTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_Should_ThrowException_When_Name_IsNullOrWhiteSpace(string name)
        {
            var exception = Assert.ThrowsAny<ArgumentException>(() =>
                    Configuration.Create(
                        name: name,
                        type: "String",
                        value: "Test",
                        applicationName: "DynamicConfiguration.UnitTests")
            );
            Assert.Contains(nameof(name), exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_Should_ThrowException_When_Type_IsNullOrWhiteSpace(string type)
        {
            var exception = Assert.ThrowsAny<ArgumentException>(() =>
                    Configuration.Create(
                        name: "Test",
                        type: type,
                        value: "Test",
                        applicationName: "DynamicConfiguration.UnitTests")
            );
            Assert.Contains(nameof(type), exception.Message);
        }

        [Theory]
        [InlineData("integer1")]
        [InlineData("string2")]
        [InlineData("array")]
        public void Create_Should_ThrowException_When_Type_InvalidPropertyType(string type)
        {
            var exception = Assert.ThrowsAny<ArgumentException>(() =>
                    Configuration.Create(
                        name: "Test",
                        type: type,
                        value: "Test",
                        applicationName: "DynamicConfiguration.UnitTests")
            );
            Assert.Contains(nameof(PropertyType), exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_Should_ThrowException_When_Value_IsNullOrWhiteSpace(string value)
        {
            var exception = Assert.ThrowsAny<ArgumentException>(() =>
                    Configuration.Create(
                        name: "Test",
                        type: "String",
                        value: value,
                        applicationName: "DynamicConfiguration.UnitTests")
            );
            Assert.Contains(nameof(value), exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_Should_ThrowException_When_AplicationName_IsNullOrWhiteSpace(string applicationName)
        {
            var exception = Assert.ThrowsAny<ArgumentException>(() =>
                    Configuration.Create(
                        name: "Test",
                        type: "String",
                        value: "Test",
                        applicationName: applicationName)
            );
            Assert.Contains(nameof(applicationName), exception.Message);
        }


        [Fact]
        public void Create_Should_Be_Successful()
        {
            string name = "Test";
            string type = "String";
            string value = "Test";
            string applicationName = "DynamicConfiguration.UnitTests";

            var user = Configuration.Create(
                name: name,
                type: type,
                value: value,
                applicationName: applicationName);

            Assert.Equal(name, user.Name);
            Assert.Equal(type, user.Type.Key);
            Assert.Equal(value, user.Value);
            Assert.Equal(applicationName, user.ApplicationName);
        }

        [Fact]
        public void Map_Should_Be_Successful()
        {
            string id = "1234567";
            bool isDeleted = false;
            var createdAt = DateTime.Now;
            var updatedAt = DateTime.Now;
            int status = 1;
            string name = "Test";
            string type = "String";
            string value = "Test";
            string applicationName = "DynamicConfiguration.UnitTests";

            var user = Configuration.Map(
                id: id,
                isDeleted: isDeleted,
                createdAt: createdAt,
                updatedAt: updatedAt,
                status: status,
                name: name,
                type: type,
                value: value,
                applicationName: applicationName);

            Assert.Equal(id, user.Id);
            Assert.Equal(isDeleted, user.IsDeleted);
            Assert.Equal(createdAt, user.CreatedAt);
            Assert.Equal(updatedAt, user.UpdatedAt);
            Assert.Equal(status, user.Status);
            Assert.Equal(name, user.Name);
            Assert.Equal(type, user.Type.Key);
            Assert.Equal(value, user.Value);
            Assert.Equal(applicationName, user.ApplicationName);
        }

        [Fact]
        public void MarkPassive_Should_Be_Successful()
        {
            var configuration = ConfigurationTestBase.GetGeneratedConfiguration();

            configuration.MarkPassive();

            Assert.Equal(BaseStatus.Passive, configuration.Status);
        }

        [Fact]
        public void MarkActive_Should_Be_Successful()
        {
            var configuration = ConfigurationTestBase.GetGeneratedConfiguration();

            configuration.MarkPassive();

            configuration.MarkActive();

            Assert.Equal(BaseStatus.Active, configuration.Status);
        }

        [Fact]
        public void MarkDelete_Should_Be_Successful()
        {
            var configuration = ConfigurationTestBase.GetGeneratedConfiguration();

            configuration.MarkDeleted();

            Assert.True(configuration.IsDeleted);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void SetName_Should_ThrowException_When_Name_IsNullOrWhiteSpace(string name)
        {
            var configuration = ConfigurationTestBase.GetGeneratedConfiguration();
            var exception = Assert.ThrowsAny<ArgumentException>(() =>
                configuration.SetName(name));
            Assert.Contains(nameof(name), exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void SetType_Should_ThrowException_When_Type_IsNullOrWhiteSpace(string type)
        {

            var configuration = ConfigurationTestBase.GetGeneratedConfiguration();
            var exception = Assert.ThrowsAny<ArgumentException>(() =>
                configuration.SetType(type));
            Assert.Contains(nameof(type), exception.Message);
        }

        [Theory]
        [InlineData("integer1")]
        [InlineData("string2")]
        [InlineData("array")]
        public void SetType_Should_ThrowException_When_Type_InvalidPropertyType(string type)
        {
            var configuration = ConfigurationTestBase.GetGeneratedConfiguration();
            var exception = Assert.ThrowsAny<ArgumentException>(() =>
                configuration.SetType(type));
            Assert.Contains(nameof(PropertyType), exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void SetValue_Should_ThrowException_When_Value_IsNullOrWhiteSpace(string value)
        {
            var configuration = ConfigurationTestBase.GetGeneratedConfiguration();
            var exception = Assert.ThrowsAny<ArgumentException>(() =>
                configuration.SetValue(value));
            Assert.Contains(nameof(value), exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void SetApplicationName_Should_ThrowException_When_ApplicationName_IsNullOrWhiteSpace(string applicationName)
        {
            var configuration = ConfigurationTestBase.GetGeneratedConfiguration();
            var exception = Assert.ThrowsAny<ArgumentException>(() =>
                configuration.SetApplicationName(applicationName));
            Assert.Contains(nameof(applicationName), exception.Message);
        }

        [Theory]
        [InlineData("Test")]
        [InlineData("SiteName")]
        [InlineData("IsBasketEnabled")]
        public void SetName_Should_Be_Successful(string name)
        {
            var configuration = ConfigurationTestBase.GetGeneratedConfiguration();
            configuration.SetName(name);
            Assert.Equal(name, configuration.Name);
        }

        [Theory]
        [InlineData("Int")]
        [InlineData("String")]
        [InlineData("Boolean")]
        public void SetType_Should_Be_Successful(string type)
        {
            var configuration = ConfigurationTestBase.GetGeneratedConfiguration();
            configuration.SetType(type);
            Assert.Equal(new PropertyType(type), configuration.Type);
        }

        [Theory]
        [InlineData("Github.com")]
        [InlineData("1")]
        [InlineData("50.5")]
        public void SetValue_Should_Be_Successful(string value)
        {
            var configuration = ConfigurationTestBase.GetGeneratedConfiguration();
            configuration.SetValue(value);
            Assert.Equal(value, configuration.Value);
        }

        [Theory]
        [InlineData("DynamicConfiguration.API")]
        [InlineData("DynamicConfiguration.WEB")]
        [InlineData("DynamicConfiguration.CacheConsumer")]
        public void SetApplicationName_Should_Be_Successful(string applicationName)
        {
            var configuration = ConfigurationTestBase.GetGeneratedConfiguration();
            configuration.SetApplicationName(applicationName);
            Assert.Equal(applicationName, configuration.ApplicationName);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetStatus_Should_Be_Successful(bool status)
        {
            var configuration = ConfigurationTestBase.GetGeneratedConfiguration();
            configuration.SetStatus(status);
            var newStatus = status ? BaseStatus.Active : BaseStatus.Passive;
            Assert.Equal(newStatus, configuration.Status);
        }
    }
}
