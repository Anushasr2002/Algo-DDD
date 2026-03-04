using System;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Entities
{
    public class EvaluationResult
    {
        public StrategyId StrategyId { get; }
        public string Symbol { get; }
        public SignalAction Action { get; }
        public decimal Price { get; }
        public string Description { get; }
        public DateTime Timestamp { get; }

        public EvaluationResult(StrategyId strategyId, string symbol, SignalAction action,
                                decimal price, string description, DateTime timestamp)
        {
            StrategyId = strategyId;
            Symbol = symbol;
            Action = action;
            Price = price;
            Description = description;
            Timestamp = timestamp;
        }
    }
}
