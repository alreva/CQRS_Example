namespace CQRS
{
    public interface ICommandSender
    {
        void Send(Command cmd);
    }
}