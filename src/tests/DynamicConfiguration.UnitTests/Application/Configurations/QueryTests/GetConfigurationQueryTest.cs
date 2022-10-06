using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DynamicConfiguration.Application.Configurations.Commands.DeleteConfiguration;
using DynamicConfiguration.Application.Configurations.Exceptions;
using DynamicConfiguration.Application.Configurations.Queries.GetConfiguration;
using DynamicConfiguration.Core.Application.Exceptions;
using DynamicConfiguration.Domain.Configurations;
using DynamicConfiguration.Domain.Configurations.Ports;
using DynamicConfiguration.UnitTests.Base;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace DynamicConfiguration.UnitTests.Application.Configurations.QueryTests
{
    public class GetConfigurationQueryTest : CommandTestBase<GetConfigurationQueryHandler>
    {

        private readonly Mock<IConfigurationDataPort> _mockConfigurationDataPort;
        public GetConfigurationQueryTest()
        {
            _mockConfigurationDataPort = _handlerMoqAssist.GetMock<IConfigurationDataPort>();
        }

        [Theory]
        [MemberData(nameof(GetConfigurationQuery))]
        public async Task GetConfigurationQueryHandler_Throws_Exception_When_Configuration_Not_Found(GetConfigurationQuery command)
        {
            _mockConfigurationDataPort.Setup(q => q.GetByIdAsync(command.Id))
                .ReturnsAsync((Configuration)null);

            await Assert.ThrowsAsync<ConfigurationNotFoundException>(async () =>
                await _handler.Handle(command, cancellationToken: CancellationToken.None)
            );
        }

        [Theory]
        [MemberData(nameof(GetConfigurationQuery))]
        public async Task GetConfigurationQueryHandler_Returns_Success_When_Configuration_Deleted(GetConfigurationQuery command)
        {
            var configuration = ConfigurationTestBase.GetGeneratedConfiguration();
            _mockConfigurationDataPort.Setup(q => q.GetByIdAsync(command.Id))
                .ReturnsAsync(configuration);

            var response = await _handler.Handle(command, cancellationToken: CancellationToken.None);

            Assert.True(response.Success);
            Assert.Equal(response.StatusCode, StatusCodes.Status200OK);
            Assert.NotNull(response.Data);
        }

        public static IEnumerable<object[]> GetConfigurationQuery()
        {
            yield return new object[]
            {
                new GetConfigurationQuery(id: "123")
            };
        }
    }
}
