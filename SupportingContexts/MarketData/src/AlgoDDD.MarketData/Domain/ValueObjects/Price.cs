using AlgoDDD.SharedKernel.Domain.BaseClasses;

namespace AlgoDDD.MarketData.Domain.ValueObjects;

public class Price : ValueObject
{
    public decimal Value { get; }
    public string Currency { get; }

    public Price(decimal value, string currency)
    {
        if (value < 0)
            throw new ArgumentException("Price cannot be negative", nameof(value));
        
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency cannot be empty", nameof(currency));

        Value = value;
        Currency = currency.ToUpperInvariant();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Currency;
    }

    public static Price operator +(Price a, Price b)
    {
        if (a.Currency != b.Currency)
            throw new InvalidOperationException($"Cannot add prices with different currencies: {a.Currency} and {b.Currency}");
        
        return new Price(a.Value + b.Value, a.Currency);
    }

    public static Price operator -(Price a, Price b)
    {
        if (a.Currency != b.Currency)
            throw new InvalidOperationException($"Cannot subtract prices with different currencies: {a.Currency} and {b.Currency}");
        
        return new Price(a.Value - b.Value, a.Currency);
    }

    public override string ToString() => $"{Value:F2} {Currency}";
}
