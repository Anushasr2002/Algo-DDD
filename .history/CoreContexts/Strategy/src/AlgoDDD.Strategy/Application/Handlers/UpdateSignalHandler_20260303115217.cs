using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.Interfaces;
using AlgoDDD.Strategy.Domain.ValueObjects;
using Skender.Stock.Indicators;
using AlgoDDD.MarketData.Entities;

namespace AlgoDDD.Strategy.Application.Handlers
{
    public class UpdateSignalCommand : IRequest<bool>
    {
        public StrategyId StrategyId { get; init; }
        public Bar LatestBar { get; init; }
        public IndicatorValues Indicators { get; init; } // renamed DTO

        public UpdateSignalCommand(StrategyId strategyId, Bar latestBar, IndicatorValues indicators)
        {
            StrategyId = strategyId;
            LatestBar = latestBar;
            Indicators = indicators;
        }
    }

    public class UpdateSignalHandler : IRequestHandler<UpdateSignalCommand, bool>
    {
        private readonly IStrategyRepository _repository;

        public UpdateSignalHandler(IStrategyRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<bool> Handle(UpdateSignalCommand request, CancellationToken cancellationToken)
        {
            // Example: compute SMA using Skender.Stock.Indicators
            var quotes = new List<Quote> { request.LatestBar.ToQuote() };
            var smaResults = quotes.GetSma(14);

            var strategy = await _repository.GetByIdAsync(request.StrategyId, cancellationToken);
            if (strategy == null) return false;

            var result = strategy.Evaluate(
                request.LatestBar,
                shortMA: request.Indicators.ShortMA,
                longMA: request.Indicators.LongMA,
                mean: request.Indicators.Mean,
                threshold: request.Indicators.Threshold,
                upperBand: request.Indicators.UpperBand,
                lowerBand: request.Indicators.LowerBand,
                rsi: request.Indicators.RSI
            );

            await _repository.SaveEvaluationResultAsync(result, cancellationToken);
            return true;
        }
    }
}
