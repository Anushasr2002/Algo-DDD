using System;
using System.Collections.Generic;
using AlgoDDD.SharedKernel;

namespace AlgoDDD.Strategy.Domain.ValueObjects
{
    public sealed class StrategyId : ValueObject
    {
        public required Guid Value { get; init; }

        public StrategyId(Guid value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override bool Equals(object? obj)
        {
            return obj is StrategyId other && Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
