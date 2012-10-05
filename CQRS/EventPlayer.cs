namespace CQRS
{
    public class EventPlayer : AdoNetQueryService, IEventPlayer
    {
        private readonly IToEventConverter _toEventConverter;
        private readonly IEventPublisher _eventPublisher;

        public EventPlayer(
            IAdoNetConnectionProvider connectionProvider,
            IToEventConverter toEventConverter,
            IEventPublisher eventPublisher) : base(connectionProvider)
        {
            _toEventConverter = toEventConverter;
            _eventPublisher = eventPublisher;
        }

        public void Play()
        {
            ExecuteDirectTableReader("EventStore", reader =>
                {
                    while (reader.Read())
                    {
                        _eventPublisher.Publish(_toEventConverter.ToEvent(reader));
                    }
                   
                    return (object) null;
                });
        }
    }
}