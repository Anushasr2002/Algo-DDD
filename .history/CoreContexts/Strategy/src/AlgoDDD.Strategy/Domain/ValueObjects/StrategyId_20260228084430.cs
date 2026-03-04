using System;

namespace AlgoDDD.Strategy.Domain.ValueObjects
{
    public class StrategyId
    {
        // Example property
        public Guid Value { get; private set; }

        // Example constructor
        public StrategyId(Guid value)
        {
            Value = value;
        }

        // Example factory method
        public static StrategyId NewId()
        {
            return new StrategyId(Guid.NewGuid());
        }

        // Example equality overrides
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
