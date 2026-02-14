namespace AlgoDDD.Strategy.Domain.ValueObjects;

public enum StrategyType
{
    MovingAverageCrossover,
    RSI,  // Relative Strength Index
    MACD, // Moving Average Convergence Divergence
    BollingerBands,
    MeanReversion,
    Momentum,
    Arbitrage,
    Custom
}
