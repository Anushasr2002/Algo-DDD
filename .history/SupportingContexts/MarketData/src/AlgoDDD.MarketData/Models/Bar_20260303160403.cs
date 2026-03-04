using System;

namespace AlgoDDD.MarketData.Models
{
    public class Bar
    {
        public DateTime Timestamp { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public long Volume { get; set; }

        // Add ToQuote method for Skender.Stock.Indicators
        public Skender.Stock.Indicators.Quote ToQuote()
        {
            return new Skender.Stock.Indicators.Quote
            {
                Date = Timestamp,
                Open = (double)Open,
                High = (double)High,
                Low = (double)Low,
                Close = (double)Close,
                Volume = Volume
            };
        }
    }
}