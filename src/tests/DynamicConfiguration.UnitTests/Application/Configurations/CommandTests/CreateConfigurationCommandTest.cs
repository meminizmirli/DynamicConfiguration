using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DynamicConfiguration.Application.Configurations.Commands.CreateConfiguration;
using DynamicConfiguration.Core.Application.Exceptions;
using DynamicConfiguration.Domain.Configurations;
using DynamicConfiguration.Domain.Configurations.Ports;
using DynamicConfiguration.UnitTests.Base;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace DynamicConfiguration.UnitTests.Application.Configurations.CommandTests
{
    public class CreateConfigurationCommandTest : CommandTestBase<CreateConfigurationCommandHandler>
    {

        private readonly Mock<IConfigurationDataPort> _mockConfigurationDataPort;
        private readonly Mock<IConfigurationRedisDataPort> _mockConfigurationRedisDataPort;
        public CreateConfigurationCommandTest()
        {
            _mockConfigurationDataPort = _handlerMoqAssist.GetMock<IConfigurationDataPort>();
            _mockConfigurationRedisDataPort = _handlerMoqAssist.GetMock<IConfigurationRedisDataPort>();
        }

        [Theory]
        [MemberData(nameof(CreateConfigurationCommand))]
        public async Task CreateConfigurationCommandHandler_Throws_Exception_When_Configuration_Cannot_Be_Created(CreateConfigurationCommand command)
        {
            _mockConfigurationDataPort.Setup(q => q.CreateAsync(It.IsAny<Configuration>()))
                .ReturnsAsync(false);

            await Assert.ThrowsAsync<OperationException>(async () =>
                await _handler.Handle(command, cancellationToken: CancellationToken.None)
            );
        }

        [Theory]
        [MemberData(nameof(CreateConfigurationCommand))]
        public async Task CreateConfigurationCommandHandler_Returns_Success_When_Configuration_Created(CreateConfigurationCommand command)
        {
            _mockConfigurationDataPort.Setup(q => q.CreateAsync(It.IsAny<Configuration>()))
                .ReturnsAsync(true);

            _mockConfigurationRedisDataPort.Setup(q => q.ClearCacheAsync());

            var response = await _handler.Handle(command, cancellationToken: CancellationToken.None);

            Assert.True(response.Success);
            Assert.Equal(response.StatusCode, StatusCodes.Status200OK);
            Assert.NotNull(response.Data);
        }

        public static IEnumerable<object[]> CreateConfigurationCommand()
        {
            yield return new object[]
            {
                new CreateConfigurationCommand
                {
                    Name = "Test",
                    Type = "String",
                    Value = "Test",
                    ApplicationName = "DynamicConfiguration.UnitTests"
                }
            };
        }
    }
}
