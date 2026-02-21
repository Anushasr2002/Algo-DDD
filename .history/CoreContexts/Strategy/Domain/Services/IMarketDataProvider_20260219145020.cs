using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.ValueObjects;
using AlgoDDD.MarketData.Domain.Entities;

namespace AlgoDDD.Strategy.Domain.Services
{
    public interface IMarketDataProvider
    {
        Task<IEnumerable<Signal>> GetSignalsAsync(StrategyId strategyId, DateTime from, DateTime to);
        Task<List<PriceBar>> GetHistoricalBarsAsync(StockSymbol symbol, int lookback);
    }
}
