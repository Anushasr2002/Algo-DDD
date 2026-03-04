using AlgoDDD.MarketData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlgoDDD.Strategy.Domain.Interfaces
{
    public interface IMarketDataProvider
    {
        Task<List<Bar>> GetHistoricalBarsAsync(string symbol, string timeframe, int count);
        Task<Bar> GetCurrentBarAsync(string symbol);
    }
}