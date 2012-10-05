using System;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace CQRS
{
    public class ToEventConverter : IToEventConverter
    {
        public Event ToEvent(IDataRecord dbRecord)
        {
            var typeIndex = dbRecord.GetOrdinal("EventType");
            var serializedDataIndex = dbRecord.GetOrdinal("EventData");

            var eventTypeName = dbRecord.GetString(typeIndex);
            var eventData = dbRecord.GetString(serializedDataIndex);

            var eventType = Type.GetType(eventTypeName);
            if (eventType == null)
            {
                throw new TypeLoadException(eventTypeName);
            }

            var serializer = new JsonSerializer();
            using (TextReader reader = new StringReader(eventData))
            {
                return (Event)serializer.Deserialize(reader, eventType);
            }
        }
    }
}