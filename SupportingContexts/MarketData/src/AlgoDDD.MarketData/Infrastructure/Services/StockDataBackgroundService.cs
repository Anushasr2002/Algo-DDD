using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AlgoDDD.MarketData.Domain.Interfaces;
using AlgoDDD.MarketData.Infrastructure.Configuration;

namespace AlgoDDD.MarketData.Infrastructure.Services;

public class StockDataBackgroundService : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly ILogger<StockDataBackgroundService> _logger;
    private readonly MarketDataConfig _config;
    private readonly List<string> _trackedSymbols = new()
    {
        "AAPL", "MSFT", "GOOGL", "AMZN", "META",  // Tech
        "JPM", "BAC", "WFC",                        // Banking
        "JNJ", "PFE", "MRK",                         // Healthcare
        "XOM", "CVX",                                 // Energy
        "SPY", "QQQ"                                   // ETFs
    };

    public StockDataBackgroundService(
        IServiceProvider services,
        IOptions<MarketDataConfig> config,
        ILogger<StockDataBackgroundService> logger)
    {
        _services = services;
        _logger = logger;
        _config = config.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Stock Data Background Service started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await UpdateStockData(stoppingToken);
                
                // Wait for next update interval (e.g., 1 minute for real-time, 5 minutes for regular updates)
                await Task.Delay(TimeSpan.FromMinutes(_config.CacheDurationMinutes), stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Stock Data Background Service");
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }

    private async Task UpdateStockData(CancellationToken stoppingToken)
    {
        using var scope = _services.CreateScope();
        var stockDataService = scope.ServiceProvider.GetRequiredService<StockDataService>();
        var repository = scope.ServiceProvider.GetRequiredService<IMarketDataRepository>();

        _logger.LogInformation("Updating stock data for {Count} symbols", _trackedSymbols.Count);

        var tasks = _trackedSymbols.Select(async symbol =>
        {
            try
            {
                var marketData = await stockDataService.GetStockQuoteAsync(symbol);
                if (marketData != null)
                {
                    await repository.AddAsync(marketData);
                    _logger.LogDebug("Updated data for {Symbol}: , Volume: {Volume:N0}", 
                        symbol, marketData.CurrentPrice.Value, marketData.Volume);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update {Symbol}", symbol);
            }
        });

        await Task.WhenAll(tasks);
    }
}
