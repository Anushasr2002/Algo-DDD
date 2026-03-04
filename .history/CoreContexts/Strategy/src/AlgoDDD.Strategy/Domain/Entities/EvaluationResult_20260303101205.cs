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
        public string Notes { get; }
        public DateTime Timestamp { get; }

        public EvaluationResult(
            StrategyId strategyId,
            string symbol,
            SignalAction action,
            decimal price,
            string notes,
            DateTime timestamp)
        {
            StrategyId = strategyId ?? throw new ArgumentNullException(nameof(strategyId));
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            Action = action;
            Price = price;
            Notes = notes ?? string.Empty;
            Timestamp = timestamp;
        }
    }
}
