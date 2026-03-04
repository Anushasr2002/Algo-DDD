using System.Collections.Generic;
using AlgoDDD.MarketData.Models;
using AlgoDDD.MarketData.Entities;

namespace AlgoDDD.MarketData.Providers
{
    public class MarketDataProvider
    {
        // Example: fetch latest bars from a data source
        public IEnumerable<Bar> GetLatestBars(string symbol, int count)
        {
            // TODO: implement actual data retrieval (API, DB, etc.)
            return new List<Bar>();
        }

        // Example: fetch a single bar
        public Bar GetLatestBar(string symbol)
        {
            // TODO: implement actual retrieval
            return new Bar
            {
                Date = System.DateTime.UtcNow,
                Open = 0,
                High = 0,
                Low = 0,
                Close = 0,
                Volume = 0
            };
        }
    }
}
