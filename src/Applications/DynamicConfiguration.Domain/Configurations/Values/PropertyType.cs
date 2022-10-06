using System;
using DynamicConfiguration.Core.Domain;

namespace DynamicConfiguration.Domain.Configurations.Values
{
    public sealed partial record PropertyType : _ImmutableValue<string>
    {
        public PropertyType(string key) : base(key) { }
        public string DisplayName => toDisplayName(this);
    }

    public sealed partial record PropertyType
    {
        private const string INTEGER = "Int";
        private const string STRING = "String";
        private const string BOOLEAN = "Boolean";
        private const string DOUBLE = "Double";

        public static readonly PropertyType Integer = new PropertyType(INTEGER);
        public static readonly PropertyType String = new PropertyType(STRING);
        public static readonly PropertyType Boolean = new PropertyType(BOOLEAN);
        public static readonly PropertyType Double = new PropertyType(DOUBLE);
 
        private string toDisplayName(PropertyType type) => type.Key switch
        {
            INTEGER => "Int",
            STRING => "String",
            BOOLEAN => "Boolean",
            DOUBLE => "Double",
            _ => throw new ArgumentOutOfRangeException(nameof(PropertyType), $"Not expected direction value: {type.Key}"),
        };

        public static PropertyType ToPropertyType(string key) => key switch
        {
            INTEGER => Integer,
            STRING => String,
            BOOLEAN => Boolean,
            DOUBLE => Double,
            _ => throw new ArgumentOutOfRangeException(nameof(PropertyType), $"Not expected direction value: {key}"),
        };

        public object ToParse(string value) => Key switch
        {
            INTEGER => int.Parse(value),
            STRING => value.ToString(),
            BOOLEAN => bool.Parse(value),
            DOUBLE => double.Parse(value),
            _ => throw new ArgumentOutOfRangeException(nameof(PropertyType), $"Not expected direction value: {value}"),
        };

        public static object ToType(string key) => key switch
        {
            INTEGER => 0,
            STRING => string.Empty,
            BOOLEAN => true,
            DOUBLE => 0d,
            _ => throw new ArgumentOutOfRangeException(nameof(PropertyType), $"Not expected direction value: {key}"),
        };
    }
}
