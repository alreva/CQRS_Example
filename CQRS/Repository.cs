using System.Linq;

namespace CQRS
{
    public class Repository<TAggregateRoot> : IRepository<TAggregateRoot>
        where TAggregateRoot : AggregateRoot, new()
    {
        private readonly IEventStore _eventStore;

        public Repository(
            IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public TAggregateRoot Get(string aggregateRootId)
        {
            var aggrerateRootType = GetAggregateRootType();
            var events = _eventStore.LoadEvents(aggrerateRootType, aggregateRootId).ToArray();
            
            var aggregateRoot = new TAggregateRoot();
            var dynamicAggregateRoot = aggregateRoot.AsDynamic();

            foreach (var evt in events)
            {
                dynamicAggregateRoot.Apply(evt);
            }

            return aggregateRoot;
        }

        private static string GetAggregateRootType()
        {
            return typeof (TAggregateRoot).Name;
        }

        public void Save(TAggregateRoot aggrerateRoot)
        {
            var aggrerateRootType = GetAggregateRootType();
            var aggregateRootId = aggrerateRoot.Id;

            var changes = aggrerateRoot.Changes.ToArray();
            if (!changes.Any())
            {
                return;
            }

            _eventStore.Save(aggrerateRootType, aggregateRootId, changes);
        }
    }
}