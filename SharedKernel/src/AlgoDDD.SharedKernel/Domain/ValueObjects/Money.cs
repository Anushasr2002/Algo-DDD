using AlgoDDD.SharedKernel.Domain.BaseClasses;

namespace AlgoDDD.SharedKernel.Domain.ValueObjects;

public class Money : ValueObject
{
    public decimal Amount { get; }
    public string Currency { get; }
    
    private Money(decimal amount, string currency)
    {
        Amount = Math.Round(amount, 2);
        Currency = currency.ToUpperInvariant();
    }
    
    public static Money FromUsd(decimal amount)
        => new(amount, "USD");
        
    public static Money FromEur(decimal amount)
        => new(amount, "EUR");
        
    public static Money FromGbp(decimal amount)
        => new(amount, "GBP");
        
    public static Money FromAmount(decimal amount, string currency)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative", nameof(amount));
            
        if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
            throw new ArgumentException("Currency must be 3-letter ISO code", nameof(currency));
            
        return new Money(amount, currency);
    }
    
    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException($"Cannot add {Currency} to {other.Currency}");
            
        return new Money(Amount + other.Amount, Currency);
    }
    
    public Money Subtract(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException($"Cannot subtract {other.Currency} from {Currency}");
            
        if (other.Amount > Amount)
            throw new InvalidOperationException("Insufficient funds");
            
        return new Money(Amount - other.Amount, Currency);
    }
    
    public Money Multiply(decimal multiplier)
        => new(Amount * multiplier, Currency);
        
    public Money Percentage(decimal percent)
        => new(Amount * percent / 100m, Currency);
    
    public bool IsGreaterThan(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("Currency mismatch");
            
        return Amount > other.Amount;
    }
    
    public bool IsLessThan(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("Currency mismatch");
            
        return Amount < other.Amount;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
    
    public override string ToString() => $"{Currency} {Amount:N2}";
}
