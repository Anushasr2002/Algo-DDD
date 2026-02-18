using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlgoDDD.MarketData.Domain.Entities;
using AlgoDDD.Strategy.Domain;
using AlgoDDD.Strategy.Domain.Services;
using NUnit.Framework;

namespace AlgoDDD.Strategy.Tests
{
    public class StrategyEngineTests
    {
        private StrategyEngine _engine;
        private FakeMarketDataProvider _provider;

        [SetUp]
        public void Setup()
        {
            _provider = new FakeMarketDataProvider();
            _engine = new StrategyEngine(_provider);
        }

        [Test]
        public async Task SMA_Crossover_ShouldReturnBuy_WhenShortMAAboveLongMA()
        {
            var strategy = new StrategyEntity(Guid.NewGuid(), "Test SMA", "desc", "params", new List<string>{"AAPL"}, DateTime.UtcNow, "SMA");
            var signals = await _engine.ExecuteStrategyAsync(strategy);

            Assert.AreEqual(SignalType.Buy, signals[0].Type);
        }

        [Test]
        public async Task MeanReversion_ShouldReturnSell_WhenPriceAboveMeanPlusThreshold()
        {
            var strategy = new StrategyEntity(Guid.NewGuid(), "Test MR", "desc", "params", new List<string>{"AAPL"}, DateTime.UtcNow, "MeanReversion");
            var signals = await _engine.ExecuteStrategyAsync(strategy);

            Assert.AreEqual(SignalType.Sell, signals[0].Type);
        }

        [Test]
        public async Task Momentum_ShouldReturnBuy_WhenCloseMuchHigherThanOpen()
        {
            var strategy = new StrategyEntity(Guid.NewGuid(), "Test Momentum", "desc", "params", new List<string>{"AAPL"}, DateTime.UtcNow, "Momentum");
            var signals = await _engine.ExecuteStrategyAsync(strategy);

            Assert.AreEqual(SignalType.Buy, signals[0].Type);
        }
    }

    // Fake provider for testing
    public class FakeMarketDataProvider : IMarketDataProvider
    {
        public Task<List<PriceBar>> GetHistoricalBarsAsync(string symbol, int count)
        {
            var bars = new List<PriceBar>();
            for (int i = 0; i < count; i++)
            {
                bars.Add(new PriceBar(DateTime.UtcNow.AddMinutes(-i), 100, 105, 95, 100 + i, 1000));
            }
            return Task.FromResult(bars);
        }
    }
}
