using System;
using System.Collections.Generic;
using AlgoDDD.SharedKernel;

namespace AlgoDDD.Strategy.Domain.ValueObjects
{
    public class StrategyId : ValueObject
    {
        public Guid Value { get; }

        public StrategyId(Guid value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}

