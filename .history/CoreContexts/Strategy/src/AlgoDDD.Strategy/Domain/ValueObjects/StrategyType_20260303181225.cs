using AlgoDDD.SharedKernel.Domain.BaseClasses;
using System.Collections.Generic;

namespace AlgoDDD.Strategy.Domain.ValueObjects
{
    public class StrategyType : ValueObject
    {
        public string Value { get; }

        public StrategyType(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}