using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using CQRS;

namespace ReadModel.Infrastructure
{
    public class ConnectionProvider : IConnectionProvider
    {
        private readonly string _connectionStringName;
        private DbProviderFactory _dbProviderFactory;

        public ConnectionProvider(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }

        public IDbConnection CreateAndOpenConnection()
        {
            var connection = Factory.CreateConnection();

            if (connection == null)
            {
                throw new InvalidOperationException();
            }

            connection.ConnectionString = ConnectionString.ConnectionString;
            connection.Open();
            return connection;
        }

        public DbProviderFactory Factory
        {
            get { return _dbProviderFactory ?? (DbProviderFactories.GetFactory(ConnectionString.ProviderName)); }
        }

        public ConnectionStringSettings ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings[_connectionStringName]; }
        }
    }
}