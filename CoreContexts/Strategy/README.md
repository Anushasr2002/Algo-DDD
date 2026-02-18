# AlgoDDD Strategy Context

This module defines trading strategies and orchestrates signal generation using domain‑driven design (DDD) principles.  
It includes the `StrategyEntity` (state + lifecycle) and `StrategyEngine` (domain service for orchestration).

---

## 📂 Structure

- **Domain/Entities**
  - `StrategyEntity.cs` → Holds strategy metadata and evaluates signals.
- **Domain/Services**
  - `StrategyEngine.cs` → Fetches market data, computes indicators, and delegates evaluation.
- **Tests**
  - `StrategyEngineTests.cs` → Unit tests for SMA, Mean Reversion, Momentum, Bollinger Bands.
  - `PriceBarFactory.cs` → Generates synthetic bullish, bearish, and sideways market scenarios.

---

## 📊 Supported Strategies

### 1. SMA Crossover
- **Formula:**  
  - ShortMA = average of last *N* closes (e.g., 10)  
  - LongMA = average of last *M* closes (e.g., 30)  
- **Signal:**  
  - Buy → ShortMA > LongMA  
  - Sell → ShortMA < LongMA  
  - Hold → Otherwise

### 2. Mean Reversion
- **Formula:**  
  - Mean = average of closes  
  - Threshold = standard deviation of closes  
- **Signal:**  
  - Sell → Close > Mean + Threshold  
  - Buy → Close < Mean − Threshold  
  - Hold → Otherwise

### 3. Momentum
- **Formula:**  
  - Compare Close vs Open  
- **Signal:**  
  - Buy → Close > Open × 1.02  
  - Sell → Close < Open × 0.98  
  - Hold → Otherwise

### 4. Bollinger Bands
- **Formula:**  
  - SMA(20) = average of last 20 closes  
  - StdDev = standard deviation of last 20 closes  
  - UpperBand = SMA + 2 × StdDev  
  - LowerBand = SMA − 2 × StdDev  
- **Signal:**  
  - Buy → Close > UpperBand  
  - Sell → Close < LowerBand  
  - Hold → Otherwise

---

## 🧪 Running Tests

1. Navigate to the Strategy test project:
   ```bash
   cd CoreContexts/Strategy/tests/AlgoDDD.Strategy.Tests
