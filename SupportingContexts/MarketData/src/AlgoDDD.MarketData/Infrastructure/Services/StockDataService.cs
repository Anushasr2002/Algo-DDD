using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AlgoDDD.MarketData.Domain.Entities;
using AlgoDDD.MarketData.Domain.ValueObjects;
using AlgoDDD.MarketData.Infrastructure.Configuration;

namespace AlgoDDD.MarketData.Infrastructure.Services;

public class StockDataService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<StockDataService> _logger;
    private readonly MarketDataConfig _config;

    public StockDataService(
        HttpClient httpClient,
        IOptions<MarketDataConfig> config,
        ILogger<StockDataService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _config = config.Value;
        
        _httpClient.BaseAddress = new Uri(_config.BaseUrl);
    }

    public async Task<MarketData?> GetStockQuoteAsync(string symbol, string exchange = "NASDAQ")
    {
        try
        {
            // Example using Alpha Vantage API
            // You can replace with your preferred API (Yahoo Finance, IEX Cloud, etc.)
            var response = await _httpClient.GetAsync(
                $"query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={_config.ApiKey}");
            
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to fetch data for {Symbol}: {StatusCode}", 
                    symbol, response.StatusCode);
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<AlphaVantageResponse>(content);

            if (data?.GlobalQuote == null || string.IsNullOrEmpty(data.GlobalQuote.Symbol))
            {
                return null;
            }

            var stockSymbol = new StockSymbol(symbol, exchange, "USD");
            
            return new MarketData(
                stockSymbol,
                new Price(ParseDecimal(data.GlobalQuote.Price), "USD"),
                new Price(ParseDecimal(data.GlobalQuote.Open), "USD"),
                new Price(ParseDecimal(data.GlobalQuote.High), "USD"),
                new Price(ParseDecimal(data.GlobalQuote.Low), "USD"),
                ParseLong(data.GlobalQuote.Volume),
                DateTime.UtcNow
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching stock data for {Symbol}", symbol);
            return null;
        }
    }

    private decimal ParseDecimal(string value)
    {
        return decimal.TryParse(value, out var result) ? result : 0;
    }

    private long ParseLong(string value)
    {
        return long.TryParse(value, out var result) ? result : 0;
    }

    private class AlphaVantageResponse
    {
        [JsonPropertyName("Global Quote")]
        public GlobalQuote? GlobalQuote { get; set; }
    }

    private class GlobalQuote
    {
        [JsonPropertyName("01. symbol")]
        public string Symbol { get; set; } = string.Empty;
        
        [JsonPropertyName("05. price")]
        public string Price { get; set; } = "0";
        
        [JsonPropertyName("02. open")]
        public string Open { get; set; } = "0";
        
        [JsonPropertyName("03. high")]
        public string High { get; set; } = "0";
        
        [JsonPropertyName("04. low")]
        public string Low { get; set; } = "0";
        
        [JsonPropertyName("06. volume")]
        public string Volume { get; set; } = "0";
        
        [JsonPropertyName("07. latest trading day")]
        public string LatestTradingDay { get; set; } = string.Empty;
    }
}
