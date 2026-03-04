using AlgoDDD.SharedKernel.Domain.BaseClasses;
using AlgoDDD.Strategy.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace AlgoDDD.Strategy.Domain.Entities
{
    public class StrategyEntity : Entity<StrategyId>
    {
        // Existing properties
        public string Name { get; private set; }
        public string Description { get; private set; }
        public StrategyType Type { get; private set; }
        public List<string> Parameters { get; private set; }
        
        // Add missing properties
        public List<string> Symbols { get; private set; } = new();
        public string TimeFrame { get; private set; }
        public decimal MaxPositionSize { get; private set; }
        public decimal StopLoss { get; private set; }
        public decimal TakeProfit { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Constructor
        public StrategyEntity(
            StrategyId id, 
            StrategyType type, 
            List<string> parameters, 
            List<string> symbols = null,
            string timeFrame = "1h",
            decimal maxPositionSize = 10000,
            decimal stopLoss = 0.02m,
            decimal takeProfit = 0.03m) : base(id)
        {
            Type = type;
            Parameters = parameters ?? new List<string>();
            Symbols = symbols ?? new List<string>();
            TimeFrame = timeFrame;
            MaxPositionSize = maxPositionSize;
            StopLoss = stopLoss;
            TakeProfit = takeProfit;
        }

        // Private constructor for EF Core
        private StrategyEntity() { }

        // Factory method
        public static StrategyEntity Create(
            string name, 
            string description, 
            StrategyType type, 
            List<string> parameters,
            List<string> symbols = null,
            string timeFrame = "1h",
            decimal maxPositionSize = 10000,
            decimal stopLoss = 0.02m,
            decimal takeProfit = 0.03m)
        {
            var entity = new StrategyEntity(
                StrategyId.New(), 
                type, 
                parameters, 
                symbols,
                timeFrame,
                maxPositionSize,
                stopLoss,
                takeProfit);
            
            entity.Name = name;
            entity.Description = description;
            return entity;
        }

        public void UpdateDetails(string name, string description)
        {
            Name = name;
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }

        // Add Evaluate method
        public EvaluationResult Evaluate(List<Bar> bars)
        {
            // Implement your evaluation logic here
            // This is a placeholder - replace with actual strategy logic
            var signal = "HOLD";
            var confidence = 0.5;
            
            // Example logic based on strategy type
            if (Type.Value == "MovingAverageCross")
            {
                // Calculate moving averages, etc.
                signal = "BUY";
                confidence = 0.75;
            }
            
            return new EvaluationResult(Id, signal, confidence);
        }

        public bool ShouldTrade()
        {
            // Implement your trading logic
            return true;
        }
    }
}