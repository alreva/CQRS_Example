namespace CQRS
{
    public interface IRepository<TAggregateRoot>
        where TAggregateRoot : AggregateRoot
    {
        TAggregateRoot Get(string aggregateRootId);
        void Save(TAggregateRoot aggrerateRoot);
    }
}