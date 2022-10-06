using Ardalis.GuardClauses;

namespace DynamicConfiguration.Core.Domain
{
    public record _ImmutableValue<TKey>
    {
        protected _ImmutableValue(TKey key) => this.Key = Guard.Against.Null(key, nameof(key));
        public TKey Key { get; private init; }
    }
}