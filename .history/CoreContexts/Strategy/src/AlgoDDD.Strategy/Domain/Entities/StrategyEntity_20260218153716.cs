using System;
using System.Collections.Generic;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Entities
{
    /// <summary>
    /// Represents a trading strategy entity that can evaluate signals
    /// and maintain a history of generated signals.
    /// </summary>
    public class StrategyEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public StrategyType Type { get; private set; }   // ✅ Value object, not string

        private readonly List<Signal> _signals = new();
        public IReadOnlyCollection<Signal> Signals => _signals.AsReadOnly();

        public Dictionary<string, object> Parameters { get; private set; }
        public List<string> Symbols { get; private set; }
        public TimeFrame TimeFrame { get; private set; }
        public decimal? MaxPositionSize { get; private set; }
        public decimal? StopLoss { get; private set; }
        public decimal? TakeProfit { get; private set; }

        public StrategyEntity(
            Guid id,
            string name,
            StrategyType type,
            string description,
            Dictionary<string, object> parameters,
            List<string> symbols,
            TimeFrame timeFrame,
            decimal? maxPositionSize = null,
            decimal? stopLoss = null,
            decimal? takeProfit = null)
        {
            Id = id;
            Name = name;
            Type = type;
            Description = description;
            Parameters = parameters;
            Symbols = symbols;
            TimeFrame = timeFrame;
            MaxPositionSize = maxPositionSize;
            StopLoss = stopLoss;
            TakeProfit = takeProfit;
        }

        /// <summary>
        /// Adds a signal to the strategy's history.
        /// </summary>
        public void AddSignal(Signal signal) => _signals.Add(signal);
    }
}
