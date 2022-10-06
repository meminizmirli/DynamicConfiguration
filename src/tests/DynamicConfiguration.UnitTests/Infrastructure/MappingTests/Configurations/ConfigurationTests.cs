using System.Collections.Generic;
using DynamicConfiguration.Domain.Configurations;
using DynamicConfiguration.Infrastructure.Mongo.Configurations.Entities;
using DynamicConfiguration.Infrastructure.Mongo.Configurations.Mappers;
using DynamicConfiguration.UnitTests.Base;
using FluentAssertions;
using Xunit;

namespace DynamicConfiguration.UnitTests.Infrastructure.MappingTests.Configurations
{
    public class ConfigurationTests
    {
        [Fact]
        public void MapToEntity_Should_Return_Null_WhenDomainIsNull()
        {
            var domain = default(Configuration);
            var result = domain.ToEntity();
            result.Should().BeNull();
        }

        [Fact]
        public void MapToDomain_Should_Return_Null_WhenEntiyIsNull()
        {
            var domain = default(ConfigurationEntity);
            var result = domain.ToDomain();
            result.Should().BeNull();
        }

        [Fact]
        public void MapToRedisDto_Should_Return_Null_WhenEntiyIsNull()
        {
            var domain = default(ConfigurationEntity);
            var result = domain.ToRedisDto();
            result.Should().BeNull();
        }

        [Theory]
        [MemberData(nameof(TestObjects))]
        public void MapToEntity_Should_Return_EquivalentEntityObject(Configuration input, ConfigurationEntity expectation)
        {
            var result = input.ToEntity();
            result.Should().BeEquivalentTo(expectation);
        }

        [Theory]
        [MemberData(nameof(TestObjects))]
        public void MapToDomain_Should_Return_EquivalentDomainObject(Configuration expectation, ConfigurationEntity input)
        {
            var result = input.ToDomain();
            result.Should().BeEquivalentTo(expectation);
        }

        [Theory]
        [MemberData(nameof(TestObjects))]
        public void WriteChanges_Should_Return_EquivalentDomainObject(Configuration expectation, ConfigurationEntity input)
        {
            var result = input.ToDomain();
            result.WriteChanges(input);
            result.Should().BeEquivalentTo(expectation);
        }

        [Theory]
        [MemberData(nameof(TestRedisObjects))]
        public void MapToRedisDto_Should_Return_EquivalentDomainObject(ConfigurationRedisDto expectation, ConfigurationEntity input)
        {
            var result = input.ToRedisDto();
            result.Should().BeEquivalentTo(expectation);
        }


        public static IEnumerable<object[]> TestObjects()
        {
            var configuration = ConfigurationTestBase.GetGeneratedConfiguration();

            yield return new object[]
            {
                configuration,
                new ConfigurationEntity
                {
                    Id = configuration.Id,
                    IsDeleted = configuration.IsDeleted,
                    CreatedAt = configuration.CreatedAt,
                    UpdatedAt = configuration.UpdatedAt,
                    Status = configuration.Status,
                    Name = configuration.Name,
                    Type = configuration.Type.Key,
                    Value = configuration.Value,
                    ApplicationName = configuration.ApplicationName
                }
            };
        }

        public static IEnumerable<object[]> TestRedisObjects()
        {
            var configuration = ConfigurationTestBase.GetGeneratedConfiguration();

            yield return new object[]
            {
                new ConfigurationRedisDto
                {
                    Name = configuration.Name,
                    Type = configuration.Type,
                    Value = configuration.Value,
                    ApplicationName = configuration.ApplicationName
                },
                new ConfigurationEntity
                {
                    Id = configuration.Id,
                    IsDeleted = configuration.IsDeleted,
                    CreatedAt = configuration.CreatedAt,
                    UpdatedAt = configuration.UpdatedAt,
                    Status = configuration.Status,
                    Name = configuration.Name,
                    Type = configuration.Type.Key,
                    Value = configuration.Value,
                    ApplicationName = configuration.ApplicationName
                }
            };
        }
    }
}
