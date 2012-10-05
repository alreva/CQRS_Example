using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace CQRS
{
    public class AdoNetConnectionProvider : IAdoNetConnectionProvider
    {
        private readonly string _connectionStringName;
        private DbProviderFactory _dbProviderFactory;

        public AdoNetConnectionProvider(string connectionStringName)
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
            get { return _dbProviderFactory ?? (DbProviderFactories.GetFactory((string) ConnectionString.ProviderName)); }
        }

        public ConnectionStringSettings ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings[_connectionStringName]; }
        }
    }
}