using System.Data;

namespace CQRS
{
    public interface IConnectionProvider
    {
        IDbConnection CreateAndOpenConnection();
    }
}