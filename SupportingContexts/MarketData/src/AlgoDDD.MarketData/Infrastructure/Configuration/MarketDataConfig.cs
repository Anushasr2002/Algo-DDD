namespace AlgoDDD.MarketData.Infrastructure.Configuration;

public class MarketDataConfig
{
    public string ApiKey { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public int CacheDurationMinutes { get; set; } = 5;
    public int MaxRequestsPerMinute { get; set; } = 5;
}
