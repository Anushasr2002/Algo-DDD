using System;

namespace AlgoDDD.Strategy.Domain.ValueObjects
{
    public class StrategyType
    {
        // Example property
        public string Name { get; private set; }

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
