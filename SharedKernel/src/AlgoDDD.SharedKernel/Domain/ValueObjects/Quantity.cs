using AlgoDDD.SharedKernel.Domain.BaseClasses;

namespace AlgoDDD.SharedKernel.Domain.ValueObjects;

public class Quantity : ValueObject
{
    public decimal Value { get; }
    public int Decimals { get; }
    
    private Quantity(decimal value, int decimals)
    {
        Value = Math.Round(value, decimals);
        Decimals = decimals;
    }
    
    public static Quantity Create(decimal value, int decimals = 0)
    {
        if (value <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(value));
            
        if (decimals < 0 || decimals > 8)
            throw new ArgumentException("Decimals must be between 0 and 8", nameof(decimals));
            
        return new Quantity(value, decimals);
    }
    
    public static Quantity FromShares(decimal shares)
        => new(shares, 0);  // Whole shares
        
    public static Quantity FromFractional(decimal amount, int decimals = 4)
        => new(amount, decimals);  // Fractional shares
        
    public static Quantity FromCoins(decimal coins)
        => new(coins, 8);  // Crypto precision
        
    public Quantity Add(Quantity other)
    {
        if (Decimals != other.Decimals)
            throw new InvalidOperationException("Precision mismatch");
            
        return new Quantity(Value + other.Value, Decimals);
    }
    
    public Quantity Subtract(Quantity other)
    {
        if (Decimals != other.Decimals)
            throw new InvalidOperationException("Precision mismatch");
            
        if (other.Value > Value)
            throw new InvalidOperationException("Insufficient quantity");
            
        return new Quantity(Value - other.Value, Decimals);
    }
    
    public Quantity MultiplyBy(decimal factor)
        => new(Value * factor, Decimals);
        
    public Money MultiplyBy(Price price)
        => Money.FromAmount(Value * price.Amount, price.Currency);
    
    public bool IsWholeNumber => Value % 1 == 0;
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Decimals;
    }
    
    public override string ToString() => Value.ToString($"F{Decimals}");
}
