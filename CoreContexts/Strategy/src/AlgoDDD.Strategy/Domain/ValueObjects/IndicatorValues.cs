namespace AlgoDDD.Strategy.Domain.ValueObjects
{
    public class IndicatorValues
    {
        public decimal ShortMA { get; init; }
        public decimal LongMA { get; init; }
        public decimal Mean { get; init; }
        public decimal Threshold { get; init; }
        public decimal UpperBand { get; init; }
        public decimal LowerBand { get; init; }
        public decimal RSI { get; init; }

        public IndicatorValues(
            decimal shortMA,
            decimal longMA,
            decimal mean,
            decimal threshold,
            decimal upperBand,
            decimal lowerBand,
            decimal rsi)
        {
            ShortMA = shortMA;
            LongMA = longMA;
            Mean = mean;
            Threshold = threshold;
            UpperBand = upperBand;
            LowerBand = lowerBand;
            RSI = rsi;
        }
    }
}
