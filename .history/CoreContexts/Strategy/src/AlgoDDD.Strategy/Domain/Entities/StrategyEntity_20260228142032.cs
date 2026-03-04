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

    public EvaluationResult Evaluate(Bar latestBar, decimal? shortMA = null, ...)
{
    return new EvaluationResult(Id, latestBar.Symbol, SignalAction.Hold,
                                latestBar.Close, "Evaluation placeholder", DateTime.UtcNow);
}

}
}
