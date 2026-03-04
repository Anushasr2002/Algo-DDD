using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.ValueObjects;
using AlgoDDD.Strategy.Domain.Services;

namespace AlgoDDD.Strategy.Infrastructure.Services
{
    /// <summary>
    /// A stubbed implementation of IMarketDataProvider.
    /// Replace with real data access logic (e.g., API calls, database queries).
    /// </summary>
    public class MarketDataProvider : IMarketDataProvider
    {
        public Task<IEnumerable<Bar>> GetHistoricalBarsAsync(string symbol, int lookback)
        {
            // Stubbed: return a simple list of dummy bars
            var bars = new List<Bar>();
            var now = DateTime.UtcNow;

            for (int i = lookback; i > 0; i--)
            {
                bars.Add(new Bar(
                    symbol,
                    now.AddMinutes(-i),
                    open: 100m,
                    high: 105m,
                    low: 95m,
                    close: 100m + i,
                    volume: 1000 + i
                ));
            }

            return Task.FromResult<IEnumerable<Bar>>(bars);
        }

        public Task<List<EvaluationResult>> GetSignalsAsync(StrategyId strategyId, DateTime from, DateTime to)
        {
            // Stubbed: return a simple list of dummy evaluation results
            var results = new List<EvaluationResult>
            {
                new EvaluationResult(strategyId, "AAPL", SignalAction.Buy, 150m, "Stubbed Buy", DateTime.UtcNow),
                new EvaluationResult(strategyId, "MSFT", SignalAction.Sell, 250m, "Stubbed Sell", DateTime.UtcNow),
                new EvaluationResult(strategyId, "GOOG", SignalAction.Hold, 2800m, "Stubbed Hold", DateTime.UtcNow)
            };

            return Task.FromResult(results);
        }
    }
}
