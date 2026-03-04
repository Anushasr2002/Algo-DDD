using AlgoDDD.SharedKernel;
using System.Collections.Generic;

namespace AlgoDDD.Strategy.Domain.ValueObjects
{
    public class StrategyId : ValueObject
    {
        public System.Guid Value { get; }

        private StrategyId(System.Guid value)
        {
            Value = value;
        }

        public static StrategyId New() => new StrategyId(System.Guid.NewGuid());
        
        public static StrategyId From(System.Guid value) => new StrategyId(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        // Implicit conversion to Guid for easier use with Entity Framework
        public static implicit operator System.Guid(StrategyId strategyId) => strategyId.Value;
        
        // Explicit conversion from Guid
        public static explicit operator StrategyId(System.Guid value) => From(value);
    }
}