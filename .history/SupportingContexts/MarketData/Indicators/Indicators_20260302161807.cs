namespace AlgoDDD.MarketData.Indicators
{
    public static class Indicators
    {
        // Example: Moving Average
        public static decimal SimpleMovingAverage(IEnumerable<decimal> prices, int period)
        {
            return prices.TakeLast(period).Average();
        }

        // Add other indicator methods here
    }
}
