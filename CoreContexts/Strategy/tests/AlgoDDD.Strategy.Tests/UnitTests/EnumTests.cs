using Xunit;
using FluentAssertions;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Tests.UnitTests;

public class StrategyTypeTests
{
    [Fact]
    public void StrategyType_ShouldHaveExpectedValues()
    {
        // Assert
        Enum.GetValues<StrategyType>().Should().HaveCount(8);
        ((int)StrategyType.MovingAverageCrossover).Should().Be(0);
        ((int)StrategyType.RSI).Should().Be(1);
        ((int)StrategyType.MACD).Should().Be(2);
        ((int)StrategyType.BollingerBands).Should().Be(3);
    }
}

public class SignalTypeTests
{
    [Fact]
    public void SignalType_ShouldHaveExpectedValues()
    {
        // Assert
        Enum.GetValues<SignalType>().Should().HaveCount(6);
        ((int)SignalType.Buy).Should().Be(0);
        ((int)SignalType.Sell).Should().Be(1);
        ((int)SignalType.StrongBuy).Should().Be(3);
    }
}

public class TimeFrameTests
{
    [Fact]
    public void TimeFrame_ShouldHaveExpectedValues()
    {
        // Assert
        Enum.GetValues<TimeFrame>().Should().HaveCount(11);
        ((int)TimeFrame.Minute1).Should().Be(1);
        ((int)TimeFrame.Hour1).Should().Be(4);
        ((int)TimeFrame.Day1).Should().Be(7);
    }
}
