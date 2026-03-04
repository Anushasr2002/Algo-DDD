using System;

namespace AlgoDDD.Strategy.Domain.Entities
{
    public class Signal
    {
        // Example properties
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Symbol { get; set; }
        public string Action { get; set; } // e.g., "Buy" or "Sell"
        public decimal Price { get; set; }

        // Example constructor
        public Signal(int id, DateTime timestamp, string symbol, string action, decimal price)
        {
            Id = id;
            Timestamp = timestamp;
            Symbol = symbol;
            Action = action;
            Price = price;
        }

        // Example method
        public override string ToString()
        {
            return $"{Timestamp}: {Action} {Symbol} at {Price}";
        }
    }
}
