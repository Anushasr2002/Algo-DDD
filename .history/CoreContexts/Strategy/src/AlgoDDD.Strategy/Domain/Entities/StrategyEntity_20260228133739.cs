using System;
using System.Collections.Generic;
using AlgoDDD.SharedKernel;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Entities
{
    public class StrategyEntity
 {
    public StrategyId Id { get; }
    public StrategyType Type { get; }
    public List<string> Symbols { get; }

    public StrategyEntity(StrategyId id, StrategyType type, List<string> symbols)
    {
        Id = id;
        Type = type;
        Symbols = symbols;
    }

    public Signal Evaluate(Bar latestBar,
                           decimal? shortMA = null,
                           decimal? longMA = null,
                           decimal? mean = null,
                           decimal? threshold = null,
                           decimal? upperBand = null,
                           decimal? lowerBand = null,
                           decimal? rsi = null)
    {
        // Simplified evaluation logic
        return new Signal(Id, latestBar.Symbol, SignalAction.Hold,
                          latestBar.Close, "Evaluation placeholder", DateTime.UtcNow);
    }
}
}
