using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Services
{
    public interface IMarketDataProvider
    {
        /// <summary>
        /// Retrieves historical OHLCV bars for a given symbol.
        /// </summary>
        Task<IEnumerable<Bar>> GetHistoricalBarsAsync(string symbol, int lookback);

        /// <summary>
        /// Retrieves evaluation results for a strategy over a given time range.
        /// </summary>
        Task<List<EvaluationResult>> GetSignalsAsync(StrategyId strategyId, DateTime from, DateTime to);
    }
}
