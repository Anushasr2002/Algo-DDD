using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlgoDDD.MarketData.Domain.Entities;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.Services;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Infrastructure.Services
{
    /// <summary>
    /// In-memory provider for testing StrategyEngine without external feeds.
    /// Generates synthetic PriceBar data and placeholder signals.
    /// </summary>
    public class InMemoryMarketDataProvider : IMarketDataProvider
    {
        private readonly Random _random = new();

        public Task<IEnumerable<Signal>> GetSignalsAsync(StrategyId strategyId, DateTime from, DateTime to)
        {
            // Generate dummy signals for testing
            var signals = new List<Signal>();

            for (int i = 0; i < 5; i++)
            {
                var type = (i % 2 == 0) ? SignalType.Buy : SignalType.Sell;
                var price = 100 + _random.Next(-10, 10);

                signals.Add(new Signal(
                    strategyId,
                    new StockSymbol($"TEST{i}"),
                    type,
                    price,
                    $"Synthetic signal {i}",
                    DateTime.UtcNow.AddMinutes(-i * 10)
                ));
            }

            return Task.FromResult<IEnumerable<Signal>>(signals);
        }

        public Task<List<PriceBar>> GetHistoricalBarsAsync(StockSymbol symbol, int lookback)
        {
            var bars = new List<PriceBar>();
            var basePrice = 100m;

            for (int i = 0; i < lookback; i++)
            {
                var close = basePrice + _random.Next(-5, 5);
                bars.Add(new PriceBar
                {
                    Symbol = symbol,
                    Date = DateTime.UtcNow.AddMinutes(-i),
                    Open = close - 1,
                    High = close + 2,
                    Low = close - 2,
                    Close = close,
                    Volume = _random.Next(1000, 5000)
                });
            }

            bars.Reverse(); // chronological order
            return Task.FromResult(bars);
        }
    }
}
