using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlgoDDD.MarketData.Domain.Entities;
using AlgoDDD.MarketData.Tests.Factories;
using AlgoDDD.Strategy.Domain;
using AlgoDDD.Strategy.Domain.Services;
using NUnit.Framework;

namespace AlgoDDD.Strategy.Tests
{
    public class StrategyEngineTests
    {
        // Existing SMA, MeanReversion, Momentum tests omitted for brevity...

        [TestCaseSource(nameof(BollingerBandsTestCases))]
        public async Task BollingerBands_ShouldReturnExpectedSignal(List<PriceBar> bars, SignalType expected)
        {
            var strategy = new StrategyEntity(Guid.NewGuid(), "BB Strategy", "desc", "params", new List<string>{"AAPL"}, DateTime.UtcNow, "BollingerBands");
            var fakeProvider = new FakeMarketDataProvider(bars);
            var engine = new StrategyEngine(fakeProvider);

            var signals = await engine.ExecuteStrategyAsync(strategy);
            Assert.AreEqual(expected, signals[0].Type);
        }

        // --- Test case sources ---
        public static IEnumerable<TestCaseData> BollingerBandsTestCases()
        {
            yield return new TestCaseData(PriceBarFactory.CreateBullishTrend(), SignalType.Buy)
                .SetName("BB_BullishTrend_ShouldReturnBuy");
            yield return new TestCaseData(PriceBarFactory.CreateBearishTrend(), SignalType.Sell)
                .SetName("BB_BearishTrend_ShouldReturnSell");
            yield return new TestCaseData(PriceBarFactory.CreateSidewaysTrend(), SignalType.Hold)
                .SetName("BB_SidewaysTrend_ShouldReturnHold");
        }
    }

    // Refactored Fake provider
    public class FakeMarketDataProvider : IMarketDataProvider
    {
        private readonly List<PriceBar> _bars;

        public FakeMarketDataProvider(List<PriceBar> bars)
        {
            _bars = bars;
        }

        public Task<List<PriceBar>> GetHistoricalBarsAsync(string symbol, int count)
        {
            var result = _bars.Count > count ? _bars.GetRange(_bars.Count - count, count) : _bars;
            return Task.FromResult(result);
        }
    }
}
