using Xunit;
using AlgoDDD.Strategy.Domain.Entities;

namespace AlgoDDD.Strategy.Tests.Domain
{
    public class SignalTests
    {
        [Fact]
        public void Signal_ShouldInitializeCorrectly()
        {
            var signal = new Signal(Guid.NewGuid(), "Buy", DateTime.UtcNow);
            Assert.Equal("Buy", signal.Type);
        }
    }
}
