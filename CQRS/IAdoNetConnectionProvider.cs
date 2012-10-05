using System.Data;

namespace CQRS
{
    public interface IAdoNetConnectionProvider
    {
        IDbConnection CreateAndOpenConnection();
    }
}