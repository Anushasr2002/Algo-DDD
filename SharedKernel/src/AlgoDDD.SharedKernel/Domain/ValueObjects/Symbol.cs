using AlgoDDD.SharedKernel.Domain.BaseClasses;

namespace AlgoDDD.SharedKernel.Domain.ValueObjects;

public class Symbol : ValueObject
{
    public string Value { get; }
    public string Exchange { get; }
    public string AssetType { get; }
    
    private Symbol(string value, string exchange, string assetType)
    {
        Value = value.ToUpperInvariant();
        Exchange = exchange.ToUpperInvariant();
        AssetType = assetType;
    }
    
    public static Symbol Create(string value, string exchange = "NYSE", string assetType = "STOCK")
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Symbol cannot be empty", nameof(value));
            
        if (value.Length > 10)
            throw new ArgumentException("Symbol too long", nameof(value));
            
        return new Symbol(value, exchange, assetType);
    }
    
    public static Symbol CreateStock(string ticker, string exchange = "NYSE")
        => new(ticker, exchange, "STOCK");
        
    public static Symbol CreateCrypto(string ticker, string exchange = "BINANCE")
        => new(ticker, exchange, "CRYPTO");
        
    public static Symbol CreateForex(string pair, string exchange = "FOREX")
        => new(pair, exchange, "FOREX");
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Exchange;
        yield return AssetType;
    }
    
    public override string ToString() => $"{Value}.{Exchange}";
}
