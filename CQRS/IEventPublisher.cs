namespace CQRS
{
    public interface IEventPublisher
    {
        void Publish(params Event[] events);
    }
}