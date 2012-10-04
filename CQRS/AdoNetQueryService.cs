using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CQRS
{
    public class AdoNetQueryService
    {
        private readonly IConnectionProvider _connectionProvider;

        protected AdoNetQueryService(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        protected TDto ExecuteDirectTableReader<TDto>(string tableName, Func<IDataReader, TDto> action)
        {
            using (var connection = _connectionProvider.CreateAndOpenConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM " + tableName;

                using (var dbReader = cmd.ExecuteReader())
                {
                    return action(dbReader);
                }
            }
        }

        protected IEnumerable<TDto> ExecuteDirectTableReaderEnumerable<TDto>(string tableName, Func<IDataReader, TDto> action)
        {
            using (var connection = _connectionProvider.CreateAndOpenConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM " + tableName;

                using (var dbReader = cmd.ExecuteReader())
                {
                    return ReturnEnumerable(action, dbReader).ToArray();
                }
            }
        }

        private static IEnumerable<TDto> ReturnEnumerable<TDto>(Func<IDataReader, TDto> action, IDataReader dbReader)
        {
            while (dbReader.Read())
            {
                yield return action(dbReader);
            }
        }
    }
}