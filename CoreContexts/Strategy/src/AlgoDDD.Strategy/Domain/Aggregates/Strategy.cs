using AlgoDDD.SharedKernel.Domain.BaseClasses;
using AlgoDDD.Strategy.Domain.ValueObjects;
using AlgoDDD.Strategy.Domain.Events;
using AlgoDDD.MarketData.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Aggregates;

public class Strategy : AggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public StrategyType Type { get; private set; }
    public Dictionary<string, object> Parameters { get; private set; }
    public List<string> Symbols { get; private set; }
    public TimeFrame TimeFrame { get; private set; }
    public bool IsActive { get; private set; }
    public decimal? MaxPositionSize { get; private set; }
    public decimal? StopLoss { get; private set; }
    public decimal? TakeProfit { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastExecutedAt { get; private set; }
    public int TotalSignals { get; private set; }
    public int SuccessfulTrades { get; private set; }
    public decimal TotalReturn { get; private set; }

    private Strategy() { } // For EF Core

    public Strategy(
        string name,
        string description,
        StrategyType type,
        Dictionary<string, object> parameters,
        List<string> symbols,
        TimeFrame timeFrame)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Description = description;
        Type = type;
        Parameters = parameters;
        Symbols = symbols;
        TimeFrame = timeFrame;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        TotalSignals = 0;
        SuccessfulTrades = 0;
        TotalReturn = 0;

        AddDomainEvent(new StrategyCreatedEvent(this));
    }

    public Signal? GenerateSignal(StockSymbol symbol, decimal currentPrice, List<MarketData> historicalData)
    {
        if (!IsActive)
            return null;

        Signal? signal = Type switch
        {
            StrategyType.MovingAverageCrossover => GenerateMACrossoverSignal(symbol, currentPrice, historicalData),
            StrategyType.RSI => GenerateRSISignal(symbol, currentPrice, historicalData),
            StrategyType.BollingerBands => GenerateBollingerBandsSignal(symbol, currentPrice, historicalData),
            _ => null
        };

        if (signal != null)
        {
            TotalSignals++;
            LastExecutedAt = DateTime.UtcNow;
            AddDomainEvent(new SignalGeneratedEvent(this, signal));
        }

        return signal;
    }

    private Signal? GenerateMACrossoverSignal(StockSymbol symbol, decimal currentPrice, List<MarketData> historicalData)
    {
        // Get parameters
        var shortPeriod = Parameters.GetValueOrDefault("ShortPeriod", 10);
        var longPeriod = Parameters.GetValueOrDefault("LongPeriod", 30);
        
        // Calculate moving averages (simplified)
        if (historicalData.Count < longPeriod)
            return null;

        var shortMA = CalculateSMA(historicalData.TakeLast((int)shortPeriod).Select(x => x.CurrentPrice.Value).ToList());
        var longMA = CalculateSMA(historicalData.TakeLast((int)longPeriod).Select(x => x.CurrentPrice.Value).ToList());

        // Generate signal based on crossover
        if (shortMA > longMA && shortMA * 0.99m < longMA) // Golden cross
        {
            return new Signal(
                symbol,
                SignalType.Buy,
                currentPrice,
                "Moving Average Golden Cross",
                DateTime.UtcNow
            );
        }
        else if (shortMA < longMA && shortMA * 1.01m > longMA) // Death cross
        {
            return new Signal(
                symbol,
                SignalType.Sell,
                currentPrice,
                "Moving Average Death Cross",
                DateTime.UtcNow
            );
        }

        return null;
    }

    private Signal? GenerateRSISignal(StockSymbol symbol, decimal currentPrice, List<MarketData> historicalData)
    {
        var period = Parameters.GetValueOrDefault("RSIPeriod", 14);
        var overbought = Parameters.GetValueOrDefault("Overbought", 70);
        var oversold = Parameters.GetValueOrDefault("Oversold", 30);

        if (historicalData.Count < period)
            return null;

        var rsi = CalculateRSI(historicalData, (int)period);

        if (rsi <= oversold)
        {
            return new Signal(
                symbol,
                SignalType.Buy,
                currentPrice,
                $"RSI Oversold: {rsi:F2}",
                DateTime.UtcNow
            );
        }
        else if (rsi >= overbought)
        {
            return new Signal(
                symbol,
                SignalType.Sell,
                currentPrice,
                $"RSI Overbought: {rsi:F2}",
                DateTime.UtcNow
            );
        }

        return null;
    }

    private Signal? GenerateBollingerBandsSignal(StockSymbol symbol, decimal currentPrice, List<MarketData> historicalData)
    {
        var period = Parameters.GetValueOrDefault("BBPeriod", 20);
        var stdDev = Parameters.GetValueOrDefault("BBStdDev", 2.0);

        if (historicalData.Count < period)
            return null;

        var (upper, middle, lower) = CalculateBollingerBands(historicalData, (int)period, (double)stdDev);

        if (currentPrice <= lower)
        {
            return new Signal(
                symbol,
                SignalType.Buy,
                currentPrice,
                "Price below lower Bollinger Band",
                DateTime.UtcNow
            );
        }
        else if (currentPrice >= upper)
        {
            return new Signal(
                symbol,
                SignalType.Sell,
                currentPrice,
                "Price above upper Bollinger Band",
                DateTime.UtcNow
            );
        }

        return null;
    }

    // Helper calculation methods
    private decimal CalculateSMA(List<decimal> prices)
    {
        return prices.Average();
    }

    private decimal CalculateRSI(List<MarketData> data, int period)
    {
        var gains = new List<decimal>();
        var losses = new List<decimal>();

        for (int i = 1; i <= period; i++)
        {
            var change = data[data.Count - i].CurrentPrice.Value - data[data.Count - i - 1].CurrentPrice.Value;
            if (change >= 0)
            {
                gains.Add(change);
                losses.Add(0);
            }
            else
            {
                gains.Add(0);
                losses.Add(Math.Abs(change));
            }
        }

        var avgGain = gains.Average();
        var avgLoss = losses.Average();

        if (avgLoss == 0)
            return 100;

        var rs = avgGain / avgLoss;
        return 100 - (100 / (1 + rs));
    }

    private (decimal upper, decimal middle, decimal lower) CalculateBollingerBands(List<MarketData> data, int period, double stdDevMultiplier)
    {
        var prices = data.TakeLast(period).Select(x => x.CurrentPrice.Value).ToList();
        var sma = prices.Average();
        
        var variance = prices.Select(p => Math.Pow((double)(p - sma), 2)).Average();
        var stdDev = (decimal)Math.Sqrt(variance) * (decimal)stdDevMultiplier;

        return (sma + stdDev, sma, sma - stdDev);
    }

    public void Activate()
    {
        IsActive = true;
        AddDomainEvent(new StrategyActivatedEvent(this));
    }

    public void Deactivate()
    {
        IsActive = false;
        AddDomainEvent(new StrategyDeactivatedEvent(this));
    }

    public void UpdatePerformance(bool success, decimal profitLoss)
    {
        if (success)
            SuccessfulTrades++;
        
        TotalReturn += profitLoss;
    }
}
