using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DynamicConfiguration.Application.Configurations.Commands.CreateConfiguration;
using DynamicConfiguration.Application.Configurations.Commands.UpdateConfiguration;
using DynamicConfiguration.Application.Configurations.Exceptions;
using DynamicConfiguration.Core.Application.Exceptions;
using DynamicConfiguration.Domain.Configurations;
using DynamicConfiguration.Domain.Configurations.Ports;
using DynamicConfiguration.UnitTests.Base;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace DynamicConfiguration.UnitTests.Application.Configurations.CommandTests
{
    public class UpdateConfigurationCommandTest : CommandTestBase<UpdateConfigurationCommandHandler>
    {

        private readonly Mock<IConfigurationDataPort> _mockConfigurationDataPort;
        private readonly Mock<IConfigurationRedisDataPort> _mockConfigurationRedisDataPort;
        public UpdateConfigurationCommandTest()
        {
            _mockConfigurationDataPort = _handlerMoqAssist.GetMock<IConfigurationDataPort>();
            _mockConfigurationRedisDataPort = _handlerMoqAssist.GetMock<IConfigurationRedisDataPort>();
        }

        [Theory]
        [MemberData(nameof(UpdateConfigurationCommand))]
        public async Task UpdateConfigurationCommandHandler_Throws_Exception_When_Configuration_Not_Found(UpdateConfigurationCommand command)
        {
            _mockConfigurationDataPort.Setup(q => q.GetByIdAsync(command.Id))
                .ReturnsAsync((Configuration)null);

            await Assert.ThrowsAsync<ConfigurationNotFoundException>(async () =>
                await _handler.Handle(command, cancellationToken: CancellationToken.None)
            );
        }

        [Theory]
        [MemberData(nameof(UpdateConfigurationCommand))]
        public async Task UpdateConfigurationCommandHandler_Throws_Exception_When_Configuration_Cannot_Be_Updated(UpdateConfigurationCommand command)
        {
            var configuration = ConfigurationTestBase.GetGeneratedConfiguration();
            _mockConfigurationDataPort.Setup(q => q.GetByIdAsync(command.Id))
                .ReturnsAsync(configuration);

            _mockConfigurationDataPort.Setup(q => q.UpdateAsync(It.IsAny<Configuration>()))
                .ReturnsAsync(false);

            await Assert.ThrowsAsync<OperationException>(async () =>
                await _handler.Handle(command, cancellationToken: CancellationToken.None)
            );
        }

        [Theory]
        [MemberData(nameof(UpdateConfigurationCommand))]
        public async Task UpdateConfigurationCommandHandler_Returns_Success_When_Configuration_Updated(UpdateConfigurationCommand command)
        {
            var configuration = ConfigurationTestBase.GetGeneratedConfiguration();
            _mockConfigurationDataPort.Setup(q => q.GetByIdAsync(command.Id))
                .ReturnsAsync(configuration);

            _mockConfigurationDataPort.Setup(q => q.UpdateAsync(It.IsAny<Configuration>()))
                .ReturnsAsync(true);

            _mockConfigurationRedisDataPort.Setup(q => q.ClearCacheAsync());

            var response = await _handler.Handle(command, cancellationToken: CancellationToken.None);

            Assert.True(response.Success);
            Assert.Equal(response.StatusCode, StatusCodes.Status200OK);
            Assert.NotNull(response.Data);
        }

        public static IEnumerable<object[]> UpdateConfigurationCommand()
        {
            yield return new object[]
            {
                new UpdateConfigurationCommand
                {
                    Name = "Test",
                    Type = "String",
                    Value = "Test",
                    ApplicationName = "DynamicConfiguration.UnitTests",
                    Id = "123",
                    Status = true,
                }
            };
        }
    }
}
