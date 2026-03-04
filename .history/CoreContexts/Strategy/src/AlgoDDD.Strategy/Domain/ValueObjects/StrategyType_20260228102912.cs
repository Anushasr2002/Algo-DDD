using System;
using System.Collections.Generic;

namespace AlgoDDD.Strategy.Domain.ValueObjects
{
    public sealed class StrategyType : ValueObject
    {
        public string Value { get; }

        public StrategyType(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override bool Equals(object obj)
        {
            return obj is StrategyType other && Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode(StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
