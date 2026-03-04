using AlgoDDD.SharedKernel;
using AlgoDDD.Strategy.Domain.ValueObjects;
using AlgoDDD.MarketData.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Entities {
{
    public class Signal : Entity<StrategyId>
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

        private Signal() : base(StrategyId.NewId())
        {
            Symbol = null!;
            Reason = null!;
        }

        public Signal(
            StrategyId id,
            StockSymbol symbol,
            SignalType type,
            decimal price,
            string reason,
            DateTime generatedAt) : base(id)
        {
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            Type = type;
            Price = price;
            Reason = reason ?? throw new ArgumentNullException(nameof(reason));
            GeneratedAt = generatedAt;
            IsExecuted = false;
        }

        public void Execute(decimal executionPrice, decimal quantity)
        {
            ExecutionPrice = executionPrice;
            Quantity = quantity;
            ExecutedAt = DateTime.UtcNow;
            IsExecuted = true;

            if (Type == SignalType.Buy || Type == SignalType.StrongBuy)
            {
                ProfitLoss = (executionPrice - Price) * quantity;
            }
            else if (Type == SignalType.Sell || Type == SignalType.StrongSell)
            {
                ProfitLoss = (Price - executionPrice) * quantity;
            }
            else if (Type == SignalType.Hold)
            {
                ProfitLoss = 0; // Hold signals don’t generate P&L directly
            }
        }

        public void Close(decimal closingPrice)
        {
            if (!IsExecuted || ExecutedAt == null)
                return;

            var quantity = Quantity ?? 0;
            if (Type == SignalType.Buy || Type == SignalType.StrongBuy)
            {
                ProfitLoss = (closingPrice - (ExecutionPrice ?? Price)) * quantity;
            }
            else if (Type == SignalType.Sell || Type == SignalType.StrongSell)
            {
                ProfitLoss = ((ExecutionPrice ?? Price) - closingPrice) * quantity;
            }
            else if (Type == SignalType.Hold)
            {
                ProfitLoss = 0;
            }
        }
    }
}
}
