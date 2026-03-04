using System;
using Skender.Stock.Indicators;  // ✅ add this

namespace AlgoDDD.MarketData.Entities
{
    public class Bar
    {
        public string Symbol { get; }
        public DateTime Timestamp { get; }
        public decimal Open { get; }
        public decimal High { get; }
        public decimal Low { get; }
        public decimal Close { get; }
        public long Volume { get; }

        public Bar(string symbol, DateTime timestamp, decimal open, decimal high, decimal low, decimal close, long volume)
        {
            Symbol = symbol;
            Timestamp = timestamp;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
        }
    }

    // ✅ Extension method in a static class
    public static class BarExtensions
    {
        public static Quote ToQuote(this Bar bar)
        {
            return new Quote
            {
                Date = bar.Timestamp,
                Open = bar.Open,
                High = bar.High,
                Low = bar.Low,
                Close = bar.Close,
                Volume = bar.Volume
            };
        }
    }
}
ar