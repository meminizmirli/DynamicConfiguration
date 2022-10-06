using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DynamicConfiguration.Application.Configurations.Commands.CreateConfiguration;
using DynamicConfiguration.Application.Configurations.Commands.DeleteConfiguration;
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
    public class DeleteConfigurationCommandTest : CommandTestBase<DeleteConfigurationCommandHandler>
    {

        private readonly Mock<IConfigurationDataPort> _mockConfigurationDataPort;
        private readonly Mock<IConfigurationRedisDataPort> _mockConfigurationRedisDataPort;
        public DeleteConfigurationCommandTest()
        {
            _mockConfigurationDataPort = _handlerMoqAssist.GetMock<IConfigurationDataPort>();
            _mockConfigurationRedisDataPort = _handlerMoqAssist.GetMock<IConfigurationRedisDataPort>();
        }

        [Theory]
        [MemberData(nameof(DeleteConfigurationCommand))]
        public async Task DeleteConfigurationCommandHandler_Throws_Exception_When_Configuration_Not_Found(DeleteConfigurationCommand command)
        {
            _mockConfigurationDataPort.Setup(q => q.GetByIdAsync(command.Id))
                .ReturnsAsync((Configuration)null);

            await Assert.ThrowsAsync<ConfigurationNotFoundException>(async () =>
                await _handler.Handle(command, cancellationToken: CancellationToken.None)
            );
        }

        [Theory]
        [MemberData(nameof(DeleteConfigurationCommand))]
        public async Task DeleteConfigurationCommandHandler_Throws_Exception_When_Configuration_Cannot_Be_Deleted(DeleteConfigurationCommand command)
        {
            var configuration = ConfigurationTestBase.GetGeneratedConfiguration();
            _mockConfigurationDataPort.Setup(q => q.GetByIdAsync(command.Id))
                .ReturnsAsync(configuration);

            _mockConfigurationDataPort.Setup(q => q.RemoveAsync(It.IsAny<Configuration>()))
                .ReturnsAsync(false);

            await Assert.ThrowsAsync<OperationException>(async () =>
                await _handler.Handle(command, cancellationToken: CancellationToken.None)
            );
        }

        [Theory]
        [MemberData(nameof(DeleteConfigurationCommand))]
        public async Task DeleteConfigurationCommandHandler_Returns_Success_When_Configuration_Deleted(DeleteConfigurationCommand command)
        {
            var configuration = ConfigurationTestBase.GetGeneratedConfiguration();
            _mockConfigurationDataPort.Setup(q => q.GetByIdAsync(command.Id))
                .ReturnsAsync(configuration);

            _mockConfigurationDataPort.Setup(q => q.RemoveAsync(It.IsAny<Configuration>()))
                .ReturnsAsync(true);

            _mockConfigurationRedisDataPort.Setup(q => q.ClearCacheAsync());

            var response = await _handler.Handle(command, cancellationToken: CancellationToken.None);

            Assert.True(response.Success);
            Assert.Equal(response.StatusCode, StatusCodes.Status200OK);
            Assert.True(response.Data);
        }

        public static IEnumerable<object[]> DeleteConfigurationCommand()
        {
            yield return new object[]
            {
                new DeleteConfigurationCommand
                {
                    Id = "123"
                }
            };
        }
    }
}
