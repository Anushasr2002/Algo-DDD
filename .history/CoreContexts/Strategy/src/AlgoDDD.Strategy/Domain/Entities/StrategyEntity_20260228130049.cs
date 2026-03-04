using System;
using System.Collections.Generic;
using AlgoDDD.SharedKernel;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Entities
{
    public class StrategyEntity : Entity
    {
        public StrategyId Id { get; }
        public StrategyType Type { get; }
        public List<string> Symbols { get; }
        public string Description { get; }

        public StrategyEntity(StrategyId id, StrategyType type, List<string> symbols, string description)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Symbols = symbols ?? new List<string>();
            Description = description ?? string.Empty;
        }

        public Signal Evaluate(
            MarketBar latestBar,
            decimal? shortMA = null,
            decimal? longMA = null,
            decimal? mean = null,
            decimal? threshold = null,
            decimal? upperBand = null,
            decimal? lowerBand = null,
            decimal? rsi = null)
        {
            // Evaluation logic based on strategy type
            // This is a simplified example and should be expanded with real logic
            switch (Type.Value)
            {
                case "SMA":
                    if (shortMA.HasValue && longMA.HasValue)
                    {
                        if (shortMA > longMA)
                            return new Signal(Id, latestBar.Symbol, SignalAction.Buy, latestBar.Close, "SMA Crossover - Buy", DateTime.UtcNow);
                        else if (shortMA < longMA)
                            return new Signal(Id, latestBar.Symbol, SignalAction.Sell, latestBar.Close, "SMA Crossover - Sell", DateTime.UtcNow);
                    }
                    break;

                case "MeanReversion":
                    if (mean.HasValue && threshold.HasValue)
                    {
                        if (latestBar.Close < mean - threshold)
                            return new Signal(Id, latestBar.Symbol, SignalAction.Buy, latestBar.Close, "Mean Reversion - Buy", DateTime.UtcNow);
                        else if (latestBar.Close > mean + threshold)
                            return new Signal(Id, latestBar.Symbol, SignalAction.Sell, latestBar.Close, "Mean Reversion - Sell", DateTime.UtcNow);
                    }
                    break;

                case "Momentum":
                    // Simplified momentum logic
                    if (latestBar.Close > latestBar.Open)
                        return new Signal(Id, latestBar.Symbol, SignalAction.Buy, latestBar.Close, "Momentum - Buy", DateTime.UtcNow);
                    else if (latestBar.Close < latestBar.Open)
                        return new Signal
    
    
}
