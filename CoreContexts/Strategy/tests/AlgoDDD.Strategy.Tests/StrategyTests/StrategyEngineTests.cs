using Xunit;
using Moq;
using FluentAssertions;
using AlgoDDD.Strategy.Domain.Aggregates;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.Services;
using AlgoDDD.Strategy.Domain.Interfaces;
using AlgoDDD.Strategy.Domain.ValueObjects;
using AlgoDDD.MarketData.Domain.Entities;
using AlgoDDD.MarketData.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Tests.StrategyTests;

public class StrategyEngineTests
{
    private readonly Mock<IStrategyRepository> _mockRepo;
    private readonly Mock<IMarketDataService> _mockMarketData;
    private readonly StrategyEngine _engine;

    public StrategyEngineTests()
    {
        _mockRepo = new Mock<IStrategyRepository>();
        _mockMarketData = new Mock<IMarketDataService>();
        _engine = new StrategyEngine(_mockRepo.Object, _mockMarketData.Object);
    }

    [Fact]
    public async Task ExecuteStrategy_WithValidData_ShouldGenerateSignals()
    {
        // Arrange
        var strategy = CreateTestStrategy();
        var symbol = new StockSymbol("AAPL", "NASDAQ", "USD");
        var marketData = new MarketData(
            symbol,
            new Price(150.0m, "USD"),
            new Price(148.0m, "USD"),
            new Price(152.0m, "USD"),
            new Price(147.0m, "USD"),
            1000000,
            DateTime.UtcNow
        );
        
        var historicalData = new List<MarketData>();
        for (int i = 0; i < 50; i++)
        {
            historicalData.Add(new MarketData(
                symbol,
                new Price(150.0m + i, "USD"),
                new Price(148.0m + i, "USD"),
                new Price(152.0m + i, "USD"),
                new Price(147.0m + i, "USD"),
                1000000,
                DateTime.UtcNow.AddMinutes(-i)
            ));
        }

        _mockMarketData.Setup(x => x.GetLatestMarketDataAsync(It.IsAny<StockSymbol>()))
            .ReturnsAsync(marketData);
        _mockMarketData.Setup(x => x.GetHistoricalDataAsync(It.IsAny<StockSymbol>(), It.IsAny<int>()))
            .ReturnsAsync(historicalData);

        // Act
        var signals = await _engine.ExecuteStrategyAsync(strategy);

        // Assert
        signals.Should().NotBeNull();
    }

    [Fact]
    public async Task BacktestStrategy_WithSampleData_ShouldReturnResults()
    {
        // Arrange
        var strategy = CreateTestStrategy();
        var symbol = new StockSymbol("AAPL", "NASDAQ", "USD");
        var historicalData = new List<MarketData>();
        
        // Create 100 days of sample data
        for (int i = 0; i < 100; i++)
        {
            historicalData.Add(new MarketData(
                symbol,
                new Price(150.0m + (i % 20), "USD"),
                new Price(148.0m + (i % 20), "USD"),
                new Price(152.0m + (i % 20), "USD"),
                new Price(147.0m + (i % 20), "USD"),
                1000000,
                DateTime.UtcNow.AddDays(-i)
            ));
        }

        _mockMarketData.Setup(x => x.GetHistoricalDataAsync(It.IsAny<StockSymbol>(), It.IsAny<int>()))
            .ReturnsAsync(historicalData);

        // Act
        var result = await _engine.BacktestStrategyAsync(
            strategy,
            DateTime.UtcNow.AddDays(-100),
            DateTime.UtcNow,
            10000m
        );

        // Assert
        result.Should().NotBeNull();
        result.InitialCapital.Should().Be(10000m);
        result.StrategyName.Should().Be("Test MA Crossover");
    }

    private Strategy CreateTestStrategy()
    {
        var parameters = new Dictionary<string, object>
        {
            { "ShortPeriod", 10 },
            { "LongPeriod", 30 }
        };

        var symbols = new List<string> { "AAPL" };

        return new Strategy(
            "Test MA Crossover",
            "Test strategy for unit tests",
            StrategyType.MovingAverageCrossover,
            parameters,
            symbols,
            TimeFrame.Day1
        );
    }
}
