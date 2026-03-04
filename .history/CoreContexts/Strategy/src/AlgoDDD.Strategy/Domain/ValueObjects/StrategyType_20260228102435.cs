using System;

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
}


        // Constructor
        public StrategyType(string name)
        {
            Name = name;
        }

        // Example equality overrides
        public override bool Equals(object obj)
        {
            if (obj is StrategyType other)
            {
                 return obj is StrategyType other && Value.Equals(other.Value);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode(StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
