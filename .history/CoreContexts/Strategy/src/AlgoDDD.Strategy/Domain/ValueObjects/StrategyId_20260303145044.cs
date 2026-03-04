using AlgoDDD.SharedKernel;

namespace AlgoDDD.Strategy.Domain.ValueObjects
{
    public class StrategyId : ValueObject
    {
        public Guid Value { get; }

        private StrategyId(Guid value)
        {
            Value = value;
        }

        public static StrategyId New() => new StrategyId(Guid.NewGuid());
        
        public static StrategyId From(Guid value) => new StrategyId(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        // Implicit conversion to Guid for easier use with Entity Framework
        public static implicit operator Guid(StrategyId strategyId) => strategyId.Value;
        
        // Explicit conversion from Guid
        public static explicit operator StrategyId(Guid value) => From(value);
    }
}