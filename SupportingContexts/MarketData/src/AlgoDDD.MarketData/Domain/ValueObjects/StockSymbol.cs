using AlgoDDD.SharedKernel.Domain.BaseClasses;

namespace AlgoDDD.MarketData.Domain.ValueObjects;

public class StockSymbol : ValueObject
{
    public string Symbol { get; }
    public string Exchange { get; }
    public string Currency { get; }

    public StockSymbol(string symbol, string exchange, string currency)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be empty", nameof(symbol));
        
        if (string.IsNullOrWhiteSpace(exchange))
            throw new ArgumentException("Exchange cannot be empty", nameof(exchange));

        Symbol = symbol.ToUpperInvariant();
        Exchange = exchange.ToUpperInvariant();
        Currency = currency.ToUpperInvariant();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Symbol;
        yield return Exchange;
        yield return Currency;
    }

    public override string ToString() => $"{Symbol}.{Exchange}";
}
