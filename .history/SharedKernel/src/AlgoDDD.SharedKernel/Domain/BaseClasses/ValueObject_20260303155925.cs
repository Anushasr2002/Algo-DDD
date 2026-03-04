using AlgoDDD.SharedKernel.Domain.BaseClasses;
using System;
using System.Collections.Generic;

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

        // Add a public factory method for EF Core
        public static StrategyId Create(Guid value) => new StrategyId(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator Guid(StrategyId strategyId) => strategyId.Value;
        
        public static explicit operator StrategyId(Guid value) => From(value);
    }
}