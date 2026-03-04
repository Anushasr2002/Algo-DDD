using System;
using System.Collections.Generic;
using AlgoDDD.SharedKernel;

namespace AlgoDDD.Strategy.Domain.ValueObjects
{
    public sealed class StrategyId
    {
        public Guid Value { get; }

        public StrategyId(Guid value)
        {
            Value = value;
        }

        // Example factory method
        public static StrategyId NewId() => new StrategyId(Guid.NewGuid());
    }
}
