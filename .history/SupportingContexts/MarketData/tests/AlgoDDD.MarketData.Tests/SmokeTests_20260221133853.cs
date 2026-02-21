using Xunit;

namespace AlgoDDD.MarketData.Tests
{
    public class MarketDataSmokeTests
    {
        [Fact] // This attribute tells xUnit this is a test
        public void Provider_CanBeInstantiated()
        {
            var provider = new InMemoryMarketDataProvider();
            Assert.NotNull(provider);
        }
    }
}
