using System;
using Xunit;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.ValueObjects;
using AlgoDDD.Strategy.Domain.Services;
using AlgoDDD.Strategy.Domain.Repositories;
using AlgoDDD.Strategy.Domain.Aggregates;

namespace AlgoDDD.Strategy.Tests
{
    public class SmokeTests
    {
        [Fact]
        public void CanInstantiateDomainEntities()
        {
            var strategyId = StrategyId.NewId();
            var entity = new StrategyEntity(strategyId, "SMA");
            Assert.NotNull(entity);

            var signal = new Signal(strategyId, new StockSymbol("TEST"), SignalType.Buy, 100m, "Test", DateTime.UtcNow);
            Assert.NotNull(signal);

            var result = new BacktestResult(strategyId, 0, 0m);
            Assert.NotNull(result);

            var aggregate = new StrategyAggregate(strategyId, "SMA");
            Assert.NotNull(aggregate);
        }

        [Fact]
        public void CanInstantiateValueObjects()
        {
            var id = StrategyId.NewId();
            Assert.NotNull(id);

            var tf = new TimeFrame("1h");
            Assert.NotNull(tf);
        }

        [Fact]
        public void CanInstantiateServices()
        {
            var provider = new InMemoryMarketDataProvider();
            var engine = new StrategyEngine(provider);
            Assert.NotNull(engine);
        }

        [Fact]
        public void CanReferenceRepositoryInterface()
        {
            IStrategyRepository repo = null; // just check type exists
            Assert.Null(repo);
        }
    }
}
