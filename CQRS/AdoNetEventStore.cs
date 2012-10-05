using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Linq;

namespace CQRS
{
    public class AdoNetEventStore : IEventStore
    {
        private readonly IAdoNetConnectionProvider _connectionProvider;
        private readonly IToEventConverter _toEventConverter;

        public AdoNetEventStore(
            IAdoNetConnectionProvider connectionProvider,
            IToEventConverter toEventConverter)
        {
            _connectionProvider = connectionProvider;
            _toEventConverter = toEventConverter;
        }

        public IEnumerable<Event> LoadEvents(string aggrerateRootType, string aggregateRootId)
        {
            return LoadEventsInternal(aggrerateRootType, aggregateRootId).ToArray();
        }

        private IEnumerable<Event> LoadEventsInternal(string aggrerateRootType, string aggregateRootId)
        {
            using (var connection = _connectionProvider.CreateAndOpenConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = string.Format(
                    "SELECT * FROM EventStore WHERE AggregateRootType = '{0}' AND AggregateRootId = '{1}' ORDER BY Id",
                    aggrerateRootType,
                    aggregateRootId);

                using (var dbReader = cmd.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        yield return _toEventConverter.ToEvent(dbReader);
                    }
                }
            }
        }

        public void Save(string aggregateRootType, string aggregateRootId, params Event[] events)
        {
            if (!events.Any())
            {
                return;
            }

            using (var connection = _connectionProvider.CreateAndOpenConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var evt in events)
                    {
                        var serializedEventData = Serialize(evt);

                        var cmd = connection.CreateCommand();
                        cmd.Transaction = transaction;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "AddEvent";

                        cmd.AddStringParameter("aggregateRootType", aggregateRootType);
                        cmd.AddStringParameter("aggregateRootId", aggregateRootId);
                        cmd.AddDateTimeParameter("eventDate", DateTime.UtcNow);
                        cmd.AddStringParameter("eventType", evt.GetType().AssemblyQualifiedName, 255);
                        cmd.AddLongStringParameter("eventData", serializedEventData);

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }
        }

        private static string Serialize(Event evt)
        {
            var serializer = new JsonSerializer();
            var sb = new StringBuilder();
            using (TextWriter writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, evt);
            }

            return sb.ToString();
        }
    }
}