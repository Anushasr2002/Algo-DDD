using System;

namespace AlgoDDD.Strategy.Domain.ValueObjects
{
    public class StrategyId
    {
        // Example property
        public Guid Value { get; private set; }

        // Constructor
        public StrategyId(Guid value)
        {
            Value = value;
        }

        // Factory method
        public static StrategyId NewId()
        {
            return new StrategyId(Guid.NewGuid());
        }

        // Equality overrides
        public override bool Equals(object obj)
        {
            if (obj is StrategyId other)
            {
                return Value.Equals(other.Value);
            }
            return false;
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
