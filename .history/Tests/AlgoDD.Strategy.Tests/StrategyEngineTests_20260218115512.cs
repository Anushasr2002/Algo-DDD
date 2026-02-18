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
        [TestCaseSource(nameof(SmaTestCases))]
        public async Task SMA_Crossover_ShouldReturnExpectedSignal(List<PriceBar> bars, SignalType expected)
        {
            var strategy = new StrategyEntity(Guid.NewGuid(), "SMA Strategy", "desc", "params", new List<string>{"AAPL"}, DateTime.UtcNow, "SMA");
            var fakeProvider = new FakeMarketDataProvider(bars);
            var engine = new StrategyEngine(fakeProvider);

            var signals = await engine.ExecuteStrategyAsync(strategy);
            Assert.AreEqual(expected, signals[0].Type);
        }

        [TestCaseSource(nameof(MeanReversionTestCases))]
        public async Task MeanReversion_ShouldReturnExpectedSignal(List<PriceBar> bars, SignalType expected)
        {
            var strategy = new StrategyEntity(Guid.NewGuid(), "MR Strategy", "desc", "params", new List<string>{"AAPL"}, DateTime.UtcNow, "MeanReversion");
            var fakeProvider = new FakeMarketDataProvider(bars);
            var engine = new StrategyEngine(fakeProvider);

            var signals = await engine.ExecuteStrategyAsync(strategy);
            Assert.AreEqual(expected, signals[0].Type);
        }

        [TestCaseSource(nameof(MomentumTestCases))]
        public async Task Momentum_ShouldReturnExpectedSignal(List<PriceBar> bars, SignalType expected)
        {
            var strategy = new StrategyEntity(Guid.NewGuid(), "Momentum Strategy", "desc", "params", new List<string>{"AAPL"}, DateTime.UtcNow, "Momentum");
            var fakeProvider = new FakeMarketDataProvider(bars);
            var engine = new StrategyEngine(fakeProvider);

            var signals = await engine.ExecuteStrategyAsync(strategy);
            Assert.AreEqual(expected, signals[0].Type);
        }

        // --- Test case sources ---
        public static IEnumerable<TestCaseData> SmaTestCases()
        {
            yield return new TestCaseData(PriceBarFactory.CreateBullishTrend(), SignalType.Buy).SetName("SMA_BullishTrend_ShouldReturnBuy");
            yield return new TestCaseData(PriceBarFactory.CreateBearishTrend(), SignalType.Sell).SetName("SMA_BearishTrend_ShouldReturnSell");
        }

        public static IEnumerable<TestCaseData> MeanReversionTestCases()
        {
            yield return new TestCaseData(PriceBarFactory.CreateBullishTrend(), SignalType.Sell).SetName("MR_BullishTrend_ShouldReturnSell");
            yield return new TestCaseData(PriceBarFactory.CreateBearishTrend(), SignalType.Buy).SetName("MR_BearishTrend_ShouldReturnBuy");
        }

        public static IEnumerable<TestCaseData> MomentumTestCases()
        {
            yield return new TestCaseData(PriceBarFactory.CreateBullishTrend(), SignalType.Buy).SetName("Momentum_BullishTrend_ShouldReturnBuy");
            yield return new TestCaseData(PriceBarFactory.CreateBearishTrend(), SignalType.Sell).SetName("Momentum_BearishTrend_ShouldReturnSell");
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
