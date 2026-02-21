namespace AlgoDDD.Strategy.Domain.Interfaces
{
    public interface IStrategyRepository
    {
        void Save(object strategy);
        object? GetById(Guid id);
    }
}
