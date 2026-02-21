using MediatR;
using AlgoDDD.Strategy.Domain.Entities;

namespace AlgoDDD.Strategy.Application.Commands
{
    // Command definition
    public sealed record CreateStrategyCommand(Guid Id, string Name) 
        : IRequest<StrategyEntity>;

    // Command handler (renamed to avoid duplication)
    public sealed class CreateStrategyCommandHandler 
        : IRequestHandler<CreateStrategyCommand, StrategyEntity>
    {
        public Task<StrategyEntity> Handle(CreateStrategyCommand request, CancellationToken cancellationToken)
        {
            var strategy = new StrategyEntity(request.Id, request.Name);
            return Task.FromResult(strategy);
        }
    }
}
