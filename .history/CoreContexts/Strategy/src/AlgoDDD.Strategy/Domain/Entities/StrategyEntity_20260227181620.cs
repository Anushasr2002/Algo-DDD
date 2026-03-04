using System;
using System.Collections.Generic;
using AlgoDDD.SharedKernel;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Entities
{
    public class StrategyEntity : Entity<StrategyId>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public StrategyType Type { get; private set; }
        public Dictionary<string, object> Parameters { get; private set; }
        public List<string> Symbols { get; private set; }
        public TimeFrame TimeFrame { get; private set; }
        public decimal? MaxPositionSize { get; private set; }
        public decimal? StopLoss { get; private set; }
        public decimal? TakeProfit { get; private set; }

        public StrategyEntity(
            StrategyId id,
            string name,
            StrategyType type,
            string description,
            Dictionary<string, object> parameters,
            List<string> symbols,
            TimeFrame timeFrame,
            decimal? maxPositionSize,
            decimal? stopLoss,
            decimal? takeProfit
        ) : base(id)
        {
            Name = name;
            Type = type;
            Description = description;
            Parameters = parameters ?? new Dictionary<string, object>();
            Symbols = symbols ?? new List<string>();
            TimeFrame = timeFrame;
            MaxPositionSize = maxPositionSize;
            StopLoss = stopLoss;
            TakeProfit = takeProfit;
        }
    }
}
