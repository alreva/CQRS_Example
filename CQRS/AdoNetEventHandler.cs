using System;
using System.Data;

namespace CQRS
{
    public abstract class AdoNetEventHandler<TEvent> : IEventHandler
        where TEvent : Event
    {
        private readonly IAdoNetConnectionProvider _connectionProvider;

        public AdoNetEventHandler(
            IAdoNetConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public Type EventType
        {
            get { return typeof (TEvent); }
        }

        public void Handle(Event evt)
        {
            if (!(evt is TEvent))
            {
                throw new InvalidOperationException();
            }

            var exactEvent = (TEvent) evt;
            using (var connection = _connectionProvider.CreateAndOpenConnection())
            {
                var cmd = connection.CreateCommand();
                Handle(exactEvent, cmd);
            }
        }

        protected abstract void Handle(TEvent evt, IDbCommand cmd);
    }
}