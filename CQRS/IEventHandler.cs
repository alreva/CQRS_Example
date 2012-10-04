using System;

namespace CQRS
{
    public interface IEventHandler
    {
        Type EventType { get; }
        void Handle(Event evt);
    }
}