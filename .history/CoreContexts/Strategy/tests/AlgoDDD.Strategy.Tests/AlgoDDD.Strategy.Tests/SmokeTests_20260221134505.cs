using Xunit;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.Services;

namespace AlgoDDD.Strategy.Tests
{
    public class StrategySmokeTests
    {
        [Fact]
        public void BacktestResult_CanBeInstantiated()
        {
            var result = new BacktestResult(Guid.NewGuid(), 10, 5000m);
            Assert.NotNull(result);
            Assert.Equal(10, result.Trades);
        }

        [Fact]
        public void MarketDataProvider_Interface_CanBeMocked()
        {
            IMarketDataProvider provider = new DummyMarketDataProvider();
            Assert.NotNull(provider);
            Assert.Equal(100m, provider.GetPrice("TEST"));
        }
    }

    // Simple dummy implementation for testing
    public class DummyMarketDataProvider : IMarketDataProvider
    {
        public decimal GetPrice(string symbol) => 100m;
    }
}
