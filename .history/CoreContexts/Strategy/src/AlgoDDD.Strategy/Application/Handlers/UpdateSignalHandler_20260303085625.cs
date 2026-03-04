using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.Interfaces;
using AlgoDDD.Strategy.Domain.ValueObjects;


namespace AlgoDDD.Strategy.Application.Handlers
{
    public class UpdateSignalCommand : IRequest<bool>
    {
        public StrategyId StrategyId { get; init; }
        public Bar LatestBar { get; init; }   // ✅ pass the bar to evaluate
        public Indicators Indicators { get; init; } // ✅ encapsulate indicator values

        public UpdateSignalCommand(StrategyId strategyId, Bar latestBar, Indicators indicators)
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
            var sma = Indicators.SimpleMovingAverage(signal.Prices, 14);
            var strategy = await _repository.GetByIdAsync(request.StrategyId, cancellationToken);
            if (strategy == null) return false;

            // ✅ Use Evaluate() instead of UpdateSignal()
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

            // ✅ Persist evaluation result (depends on repository design)
            await _repository.SaveEvaluationResultAsync(result, cancellationToken);

            return true;
        }
    }
}
