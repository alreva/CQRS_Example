using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS
{
    public class ReadModelRemover : IReadModelRemover
    {
        private readonly IAdoNetConnectionProvider _connectionProvider;

        public ReadModelRemover(
            IAdoNetConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public void Remove()
        {
            using (var connection = _connectionProvider.CreateAndOpenConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"TRUNCATE TABLE CatalogTree
TRUNCATE TABLE AvailableCategories";

                cmd.ExecuteNonQuery();
            }
        }
    }
}
