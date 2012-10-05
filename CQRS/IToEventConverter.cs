using System.Data;

namespace CQRS
{
    public interface IToEventConverter
    {
        Event ToEvent(IDataRecord dbRecord);
    }
}