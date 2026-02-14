namespace AlgoDDD.SharedKernel.Domain.ValueObjects;

public class Money
{
    public decimal Amount { get; }
    public string Currency { get; }
    
    public Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }
    
    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("Currency mismatch");
            
        return new Money(Amount + other.Amount, Currency);
    }
}
