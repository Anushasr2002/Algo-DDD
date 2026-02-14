using AlgoDDD.SharedKernel.Domain.BaseClasses;

namespace AlgoDDD.SharedKernel.Domain.ValueObjects;

public class Price : ValueObject
{
    public decimal Amount { get; }
    public string Currency { get; }
    public DateTime Timestamp { get; }
    
    private Price(decimal amount, string currency, DateTime timestamp)
    {
        Amount = amount;
        Currency = currency;
        Timestamp = timestamp;
    }
    
    public static Price Create(decimal amount, string currency, DateTime? timestamp = null)
    {
        if (amount <= 0)
            throw new ArgumentException("Price must be positive", nameof(amount));
            
        return new Price(amount, currency, timestamp ?? DateTime.UtcNow);
    }
    
    public static Price FromMarket(decimal bid, decimal ask, string currency)
    {
        var mid = (bid + ask) / 2;
        return new Price(mid, currency, DateTime.UtcNow);
    }
    
    public bool IsBidValid(decimal bid)
        => bid > 0 && bid < Amount * 1.5m; // Bid not 50% above last
        
    public bool IsAskValid(decimal ask)
        => ask > 0 && ask > Amount * 0.5m; // Ask not 50% below last
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
        yield return Timestamp;
    }
    
    public override string ToString() => $"{Currency} {Amount:N4} @ {Timestamp:yyyy-MM-dd HH:mm:ss}";
}
