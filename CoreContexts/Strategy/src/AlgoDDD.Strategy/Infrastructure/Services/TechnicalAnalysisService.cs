using AlgoDDD.MarketData.Domain.Entities;

namespace AlgoDDD.Strategy.Infrastructure.Services;

public class TechnicalAnalysisService
{
    public decimal CalculateSMA(List<decimal> prices, int period)
    {
        if (prices.Count < period)
            return 0;
        
        return prices.TakeLast(period).Average();
    }

    public decimal CalculateEMA(List<decimal> prices, int period)
    {
        if (prices.Count < period)
            return 0;

        var multiplier = 2.0m / (period + 1);
        var ema = prices.Take(period).Average();

        for (int i = period; i < prices.Count; i++)
        {
            ema = (prices[i] - ema) * multiplier + ema;
        }

        return ema;
    }

    public decimal CalculateRSI(List<StockData> data, int period)
    {
        if (data.Count < period + 1)
            return 50;

        var gains = new List<decimal>();
        var losses = new List<decimal>();

        for (int i = data.Count - period; i < data.Count; i++)
        {
            if (i == 0) continue;
            
            var change = data[i].CurrentPrice.Value - data[i - 1].CurrentPrice.Value;
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

        if (!gains.Any() || !losses.Any())
            return 50;

        var avgGain = gains.Average();
        var avgLoss = losses.Average();

        if (avgLoss == 0)
            return 100;

        var rs = avgGain / avgLoss;
        return 100 - (100 / (1 + rs));
    }

    public (decimal upper, decimal middle, decimal lower) CalculateBollingerBands(List<decimal> prices, int period, decimal stdDevMultiplier = 2)
    {
        var sma = CalculateSMA(prices, period);
        
        var squares = prices.TakeLast(period).Select(p => Math.Pow((double)(p - sma), 2));
        var variance = squares.Average();
        var stdDev = (decimal)Math.Sqrt(variance) * stdDevMultiplier;

        return (sma + stdDev, sma, sma - stdDev);
    }

    public (decimal macd, decimal signal, decimal histogram) CalculateMACD(List<decimal> prices, int fastPeriod = 12, int slowPeriod = 26, int signalPeriod = 9)
    {
        var fastEMA = CalculateEMA(prices, fastPeriod);
        var slowEMA = CalculateEMA(prices, slowPeriod);
        var macdLine = fastEMA - slowEMA;
        
        var macdHistory = new List<decimal>();
        for (int i = 0; i < signalPeriod; i++)
        {
            macdHistory.Add(macdLine - (i * 0.1m));
        }
        var signalLine = macdHistory.Any() ? macdHistory.Average() : 0;
        
        var histogram = macdLine - signalLine;

        return (macdLine, signalLine, histogram);
    }
}
