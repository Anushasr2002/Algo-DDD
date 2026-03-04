using System;
using System.Collections.Generic;
using AlgoDDD.SharedKernel;
using AlgoDDD.Strategy.Domain.ValueObjects;


    
    {
        public string Name { get; private set; }
        public required string Description { get; init; }
        public required StrategyType Type { get; init; }
        public required TimeFrame TimeFrame { get; init; }
        public Dictionary<string, object> Parameters { get; private set; }
        public List<string> Symbols { get; private set; }
        public decimal? MaxPositionSize { get; private set; }
        public decimal? StopLoss { get; private set; }
        public decimal? TakeProfit { get; private set; }

        public StrategyEntity(
            StrategyId id,
            string name,
            StrategyType type,
            string description,
            Dictionary<string, object>? parameters,
            List<string>? symbols,
            TimeFrame timeFrame,
            decimal? maxPositionSize,
            decimal? stopLoss,
            decimal? takeProfit
        ) : base(id)
        {
            Name = name;
            Type = type;
            Description = description;
            TimeFrame = timeFrame;
            Parameters = parameters ?? new Dictionary<string, object>();
            Symbols = symbols ?? new List<string>();
            MaxPositionSize = maxPositionSize;
            StopLoss = stopLoss;
            TakeProfit = takeProfit;
        }

        // Domain methods (stubs for now)
        public void UpdateSignal(Signal signal)
        {
            // TODO: implement domain logic
        }

        public EvaluationResult Evaluate(MarketData data)
        {
            // TODO: implement domain logic
            return new EvaluationResult();
        }
    }
}
