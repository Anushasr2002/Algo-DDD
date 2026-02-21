namespace AlgoDDD.Strategy.Domain.Services
{
    public interface IMarketDataProvider
    {
        decimal GetPrice(string symbol);
    }
}
