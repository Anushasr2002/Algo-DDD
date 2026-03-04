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
        public string Signal { get; init; }

        public UpdateSignalCommand(StrategyId strategyId, string signal)
        {
            StrategyId = strategyId;
            Signal = signal;
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
                    // UpdateSignalHandler.cs
            var result = strategy.Evaluate(
    latestBar,
    shortMA: indicators.ShortMA,
    longMA: indicators.LongMA,
    mean: indicators.Mean,
    threshold: indicators.Threshold,
    upperBand: indicators.UpperBand,
    lowerBand: indicators.LowerBand,
    rsi: indicators.RSI
);

// Use EvaluationResult instead of Signal

            var strategy = await _repository.GetByIdAsync(request.StrategyId, cancellationToken);
            if (strategy == null) return false;

            strategy.UpdateSignal(request.Signal);

            await _repository.UpdateAsync(strategy, cancellationToken);
            return true;
        }
    }
}
