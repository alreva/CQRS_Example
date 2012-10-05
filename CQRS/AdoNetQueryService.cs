using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CQRS
{
    public class AdoNetQueryService
    {
        private readonly IAdoNetConnectionProvider _connectionProvider;

        protected AdoNetQueryService(IAdoNetConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        protected TDto ExecuteDirectTableReader<TDto>(string tableName, Func<IDataReader, TDto> action)
        {
            return ExecuteReader("SELECT * FROM " + tableName, action);
        }

        protected IEnumerable<TDto> ExecuteDirectTableReaderEnumerable<TDto>(string tableName, Func<IDataReader, TDto> action)
        {
            return ExecuteReaderEnumerable("SELECT * FROM " + tableName, action);
        }

        protected TDto ExecuteRecordByIdReader<TDto>(string tableName, string id, Func<IDataReader, TDto> action)
        {
            return ExecuteReader(string.Format("SELECT * FROM " + tableName + " WHERE Id ='{0}'", id), action);
        }

        private TDto ExecuteReader<TDto>(string commandtext, Func<IDataReader, TDto> action)
        {
            using (var connection = _connectionProvider.CreateAndOpenConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = commandtext;

                using (var dbReader = cmd.ExecuteReader())
                {
                    return dbReader.Read() ? action(dbReader) : default(TDto);
                }
            }
        }

        private IEnumerable<TDto> ExecuteReaderEnumerable<TDto>(string commandtext, Func<IDataReader, TDto> action)
        {
            using (var connection = _connectionProvider.CreateAndOpenConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = commandtext;

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