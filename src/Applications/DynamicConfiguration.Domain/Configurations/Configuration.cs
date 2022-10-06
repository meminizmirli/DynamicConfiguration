using System;
using Ardalis.GuardClauses;
using DynamicConfiguration.Core.Domain;
using DynamicConfiguration.Core.Domain.Constants;
using DynamicConfiguration.Domain.Configurations.Values;

namespace DynamicConfiguration.Domain.Configurations
{
    public class Configuration : _ActivityDomain<string>
    {
        private Configuration() { }
        private Configuration(string id, bool isDeleted, DateTime createdAt, DateTime updatedAt, int status) : base(id, isDeleted, createdAt, updatedAt, status) { }

        public string Name { get; private set; }
        public PropertyType Type { get; private set; }
        public string Value { get; private set; }
        public string ApplicationName { get; private set; }

        public static Configuration Create(
            string name,
            string type,
            string value,
            string applicationName)
        {
            return new Configuration
            {
                Name = Guard.Against.NullOrWhiteSpace(name, nameof(name)),
                Type = PropertyType.ToPropertyType(Guard.Against.NullOrWhiteSpace(type, nameof(type))),
                Value = Guard.Against.NullOrWhiteSpace(value, nameof(value)),
                ApplicationName = Guard.Against.NullOrWhiteSpace(applicationName, nameof(applicationName)),
                Status = BaseStatus.Active
            };
        }

        public static Configuration Map(
            string id,
            bool isDeleted,
            DateTime createdAt,
            DateTime updatedAt,
            int status,
            string name,
            string type,
            string value,
            string applicationName)
        {
            return new Configuration(id, isDeleted, createdAt, updatedAt, status)
            {
                Name = name,
                Type = PropertyType.ToPropertyType(type),
                Value = value,
                ApplicationName = applicationName
            };
        }

        public void SetName(string name) => Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        public void SetType(string type) => Type = PropertyType.ToPropertyType(Guard.Against.NullOrWhiteSpace(type, nameof(type)));
        public void SetValue(string value) => Value = Guard.Against.NullOrWhiteSpace(value, nameof(value));
        public void SetApplicationName(string applicationName) => ApplicationName = Guard.Against.NullOrWhiteSpace(applicationName, nameof(applicationName));
        public void SetStatus(bool status) => Status = status ? BaseStatus.Active : BaseStatus.Passive;
    }
}
