# Algo-DDD: Enterprise Algorithmic Trading Platform

## 🏗 Architecture
Domain-Driven Design with Microservices

## 📦 Bounded Contexts
- **Trading**: Order management and execution
- **MarketData**: Real-time and historical data
- **RiskManagement**: Position sizing and risk limits
- **Portfolio**: Performance tracking
- **Accounting**: P&L and capital management

## 🚀 Getting Started
See /Docs/Guides/DevelopmentSetup.md

## 📚 Documentation
- Architecture decisions: /Docs/Architecture
- Domain language: /Docs/Domain/UbiquitousLanguage
- API specs: /Docs/API

## 🛠 Tech Stack
- Backend: .NET 8/C# (primary), Python (ML models)
- Frontend: React/TypeScript
- Infrastructure: Docker/Kubernetes
- Message Broker: Kafka/RabbitMQ
- Database: PostgreSQL (per service)
- Cache: Redis
