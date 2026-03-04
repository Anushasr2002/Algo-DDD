using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Infrastructure.Services
{
    public class TechnicalAnalysisService
    {
        // Example: calculate moving average
        public decimal CalculateMovingAverage(List<decimal> prices, int period)
        {
            if (prices == null || prices.Count < period)
                throw new ArgumentException("Not enough price data for the given period.");

            decimal sum = 0;
            for (int i = prices.Count - period; i < prices.Count; i++)
            {
                sum += prices[i];
            }

            return sum / period;
        }

        // Example: calculate relative strength index (RSI)
        public decimal CalculateRsi(List<decimal> prices, int period)
        {
            if (prices == null || prices.Count < period + 1)
                throw new ArgumentException("Not enough price data for RSI calculation.");

            decimal gains = 0, losses = 0;
            for (int i = prices.Count - period; i < prices.Count; i++)
            {
                var change = prices[i] - prices[i - 1];
                if (change > 0) gains += change;
                else losses -= change;
            }

            decimal rs = gains / (losses == 0 ? 1 : losses);
            return 100 - (100 / (1 + rs));
        }
    }
}
