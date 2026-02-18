using System.Collections.Generic;
using System.Threading.Tasks;
using AlgoDDD.MarketData.Domain.Entities;
using AlgoDDD.MarketData.Tests.Factories;

namespace AlgoDDD.Strategy.Tests
{
    /// <summary>
    /// Fake provider that uses PriceBarFactory to supply synthetic market data.
    /// </summary>
    public class FakeMarketDataProvider : IMarketDataProvider
    {
        private readonly List<PriceBar> _bars;

        public FakeMarketDataProvider(List<PriceBar> bars)
        {
            _bars = bars;
        }

        public Task<List<PriceBar>> GetHistoricalBarsAsync(string symbol, int count)
        {
            // Return either the injected bars or truncate to requested count
            var result = _bars.Count > count ? _bars.GetRange(_bars.Count - count, count) : _bars;
            return Task.FromResult(result);
        }
    }
}
