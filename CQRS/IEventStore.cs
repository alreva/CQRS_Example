using System.Collections.Generic;

namespace CQRS
{
    public interface IEventStore
    {
        IEnumerable<Event> LoadEvents(string aggrerateRootType, string aggregateRootId);
        void Save(string aggrerateRootType, string aggregateRootId, params Event[] events);
    }
}