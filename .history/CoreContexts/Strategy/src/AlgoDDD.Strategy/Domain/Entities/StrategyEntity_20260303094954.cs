using System;
using System.Collections.Generic;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Entities
{
    public class StrategyEntity
    {
        public StrategyId Id { get; }
        public StrategyType Type { get; }
        public List<string> Symbols { get; }
        {
        public Guid Id { get; set; } 
        public string Name { get; set; } = string.Empty;
         public string Description { get; set; } = string.Empty; 
         public string TimeFrame { get; set; } = string.Empty; 
         public int MaxPositionSize { get; set; } public decimal StopLoss { get; set; } public decimal TakeProfit { get; set; }}
        public Guid Id { get; set; } public string Name { get; set; } = string.Empty; public string Description { get; set; } = string.Empty; public string TimeFrame { get; set; } = string.Empty; public int MaxPositionSize { get; set; } public decimal StopLoss { get; set; } public decimal TakeProfit { get; set; }
        public StrategyEntity(StrategyId id, StrategyType type, List<string> symbols)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Symbols = symbols ?? new List<string>();
        }

        /// <summary>
        /// Evaluates a trading signal based on the latest bar and optional indicators.
        /// Returns an EvaluationResult instead of Signal.
        /// </summary>
        public EvaluationResult Evaluate(
            Bar latestBar,
            decimal? shortMA = null,
            decimal? longMA = null,
            decimal? mean = null,
            decimal? threshold = null,
            decimal? upperBand = null,
            decimal? lowerBand = null,
            decimal? rsi = null)
        {
            // Simplified placeholder logic: always return Hold
            return new EvaluationResult(
                Id,
                latestBar.Symbol,
                SignalAction.Hold,
                latestBar.Close,
                "Evaluation placeholder",
                DateTime.UtcNow
            );
        }
    }
}
