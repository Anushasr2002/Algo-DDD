using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Services
{
    public interface IMarketDataProvider
    {
        Task<IEnumerable<Bar>> GetHistoricalBarsAsync(string symbol, int lookback);
        Task<List<Signal>> GetSignalsAsync(StrategyId strategyId, DateTime from, DateTime to);
    }
}
