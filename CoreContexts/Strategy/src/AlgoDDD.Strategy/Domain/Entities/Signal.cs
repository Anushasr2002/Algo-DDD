using AlgoDDD.SharedKernel.Domain.BaseClasses;
using AlgoDDD.Strategy.Domain.ValueObjects;
using AlgoDDD.MarketData.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Entities;

public class Signal : Entity
{
    public StockSymbol Symbol { get; private set; }
    public SignalType Type { get; private set; }
    public decimal Price { get; private set; }
    public string Reason { get; private set; }
    public DateTime GeneratedAt { get; private set; }
    public DateTime? ExecutedAt { get; private set; }
    public decimal? ExecutionPrice { get; private set; }
    public bool IsExecuted { get; private set; }
    public decimal? Quantity { get; private set; }
    public decimal? ProfitLoss { get; private set; }

    public Signal(
        StockSymbol symbol,
        SignalType type,
        decimal price,
        string reason,
        DateTime generatedAt)
    {
        Id = Guid.NewGuid().ToString();
        Symbol = symbol;
        Type = type;
        Price = price;
        Reason = reason;
        GeneratedAt = generatedAt;
        IsExecuted = false;
    }

    public void Execute(decimal executionPrice, decimal quantity)
    {
        ExecutionPrice = executionPrice;
        Quantity = quantity;
        ExecutedAt = DateTime.UtcNow;
        IsExecuted = true;
        
        // Calculate P&L (simplified)
        if (Type == SignalType.Buy || Type == SignalType.StrongBuy)
        {
            ProfitLoss = (executionPrice - Price) * quantity;
        }
        else if (Type == SignalType.Sell || Type == SignalType.StrongSell)
        {
            ProfitLoss = (Price - executionPrice) * quantity;
        }
    }

    public void Close(decimal closingPrice)
    {
        if (!IsExecuted || ExecutedAt == null)
            return;

        // Update P&L based on closing price
        var quantity = Quantity ?? 0;
        if (Type == SignalType.Buy || Type == SignalType.StrongBuy)
        {
            ProfitLoss = (closingPrice - (ExecutionPrice ?? Price)) * quantity;
        }
        else if (Type == SignalType.Sell || Type == SignalType.StrongSell)
        {
            ProfitLoss = ((ExecutionPrice ?? Price) - closingPrice) * quantity;
        }
    }
}
