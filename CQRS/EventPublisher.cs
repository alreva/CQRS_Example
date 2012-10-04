using System;
using System.Collections.Generic;
using System.Linq;

namespace CQRS
{
    public class EventPublisher : IEventPublisher
    {
        private readonly Dictionary<Type, IEventHandler[]> _eventHandlers;

        public EventPublisher(
            params IEventHandler[] eventHandlers)
        {
            _eventHandlers = eventHandlers
                .GroupBy(evtH => evtH.EventType)
                .ToDictionary(grp => grp.Key, grp => grp.ToArray());
        }

        public void Publish(params Event[] events)
        {
            foreach (var evt in events)
            {
                var allHandlers = _eventHandlers[evt.GetType()];
                foreach (var eventHandler in allHandlers)
                {
                    eventHandler.Handle(evt);
                }
            }
        }
    }
}