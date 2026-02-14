using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Memory;
using AlgoDDD.MarketData.Domain.Entities;
using AlgoDDD.MarketData.Domain.Interfaces;
using AlgoDDD.MarketData.Domain.ValueObjects;

namespace AlgoDDD.MarketData.Infrastructure.Repositories;

public class InMemoryMarketDataRepository : IMarketDataRepository
{
    private readonly ConcurrentDictionary<string, MarketData> _latestData = new();
    private readonly ConcurrentDictionary<string, List<MarketData>> _history = new();
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

    public InMemoryMarketDataRepository(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Task<MarketData?> GetLatestBySymbolAsync(StockSymbol symbol)
    {
        var key = symbol.ToString();
        
        // Try cache first
        if (_cache.TryGetValue<MarketData>(key, out var cachedData))
        {
            return Task.FromResult(cachedData);
        }
        
        // Then check dictionary
        _latestData.TryGetValue(key, out var data);
        
        // Add to cache if found
        if (data != null)
        {
            _cache.Set(key, data, _cacheDuration);
        }
        
        return Task.FromResult(data);
    }

    public Task<IEnumerable<MarketData>> GetHistoryBySymbolAsync(StockSymbol symbol, DateTime from, DateTime to)
    {
        var key = symbol.ToString();
        
        if (_history.TryGetValue(key, out var history))
        {
            var filtered = history.Where(x => x.Timestamp >= from && x.Timestamp <= to)
                                  .OrderByDescending(x => x.Timestamp);
            return Task.FromResult(filtered);
        }
        
        return Task.FromResult(Enumerable.Empty<MarketData>());
    }

    public Task AddAsync(MarketData marketData)
    {
        var key = marketData.Symbol.ToString();
        
        // Update latest data
        _latestData.AddOrUpdate(key, marketData, (_, _) => marketData);
        
        // Add to cache
        _cache.Set(key, marketData, _cacheDuration);
        
        // Add to history
        var history = _history.GetOrAdd(key, _ => new List<MarketData>());
        history.Add(marketData);
        
        // Keep only last 100 records per symbol
        if (history.Count > 100)
        {
            history.RemoveRange(0, history.Count - 100);
        }
        
        return Task.CompletedTask;
    }

    public Task UpdateAsync(MarketData marketData)
    {
        return AddAsync(marketData); // Same logic for update
    }

    public Task DeleteAsync(string id)
    {
        var item = _latestData.FirstOrDefault(x => x.Value.Id == id);
        if (!string.IsNullOrEmpty(item.Key))
        {
            _latestData.TryRemove(item.Key, out _);
            _cache.Remove(item.Key);
        }
        
        return Task.CompletedTask;
    }
}
