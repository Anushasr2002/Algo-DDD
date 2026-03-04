using AlgoDDD.MarketData.Models;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Skender.Stock.Indicators;

namespace AlgoDDD.Strategy.Application.Handlers
{
    public class UpdateSignalHandler : IRequestHandler<UpdateSignalCommand, Unit>
    {
        private readonly IStrategyRepository _strategyRepository;
        private readonly IMarketDataProvider _marketDataProvider;

        public UpdateSignalHandler(IStrategyRepository strategyRepository, IMarketDataProvider marketDataProvider)
        {
            _strategyRepository = strategyRepository;
            _marketDataProvider = marketDataProvider;
        }

        public async Task<Unit> Handle(UpdateSignalCommand request, CancellationToken cancellationToken)
        {
            var strategy = await _strategyRepository.GetByIdAsync(request.StrategyId, cancellationToken);
            if (strategy == null)
                return Unit.Value;

            var bars = await _marketDataProvider.GetHistoricalBarsAsync(request.Symbol, request.TimeFrame, 100);
            
            // Convert bars to quotes for indicator calculation
            var quotes = bars.Select(b => b.ToQuote()).ToList();
            
            // Calculate indicators
            var rsi = quotes.GetRsi(14).LastOrDefault();
            
            // Evaluate strategy
            var result = strategy.Evaluate(bars);
            
            // Save result
            await _strategyRepository.SaveEvaluationResultAsync(result, cancellationToken);
            
            return Unit.Value;
        }
    }

    public class UpdateSignalCommand : IRequest
    {
        public StrategyId StrategyId { get; set; }
        public string Symbol { get; set; }
        public string TimeFrame { get; set; }
    }
}