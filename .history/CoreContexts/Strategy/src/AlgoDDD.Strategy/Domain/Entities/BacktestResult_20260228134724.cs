using System;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Entities
{
    public class BacktestResult { public StrategyId StrategyId { get; } public decimal ProfitLoss { get; } public int TradesExecuted { get; } public BacktestResult(StrategyId strategyId, decimal profitLoss, int tradesExecuted) { StrategyId = strategyId; ProfitLoss = profitLoss; TradesExecuted = tradesExecuted; } }
}
