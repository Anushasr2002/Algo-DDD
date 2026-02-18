using Xunit;
using AlgoDDD.Strategy.Domain.Entities;

namespace AlgoDDD.Strategy.Tests.Domain
{
    public class StrategyTests
    {
        [Fact]
        public void StrategyEntity_ShouldInitializeCorrectly()
        {
            var strategy = new StrategyEntity(Guid.NewGuid(), "Test Strategy");
            Assert.Equal("Test Strategy", strategy.Name);
        }
    }
}
