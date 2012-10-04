using System.Collections.Generic;
using System.Data;
using System.Linq;
using CQRS;

namespace ReadModel.Infrastructure
{
    public class GetCatalogTreeQueryService : AdoNetQueryService, IGetCatalogTreeQueryService
    {
        public GetCatalogTreeQueryService(IConnectionProvider connectionProvider)
            : base(connectionProvider)
        {
        }

        public CategoryDto GetCatalogTree()
        {
            return ExecuteDirectTableReader("CatalogTree", dbReader =>
                {
                    CategoryDataRow[] categoryRows = ReadCategories(dbReader).ToArray();
                    return BuildHierarchy(categoryRows);
                });
        }

        private static CategoryDto BuildHierarchy(CategoryDataRow[] categoryRows)
        {
            return new CategoryDto(null, null, GetChildren(null, categoryRows).ToArray());
        }

        private static IEnumerable<CategoryDto> GetChildren(string parentId, CategoryDataRow[] categoryRows)
        {
            return categoryRows
                .Where(c => c.ParentId == parentId)
                .Select(c => new CategoryDto(c.Id, c.Title, GetChildren(c.Id, categoryRows).ToArray()));
        }

        private static IEnumerable<CategoryDataRow> ReadCategories(IDataReader dbReader)
        {
            while (dbReader.Read())
            {
                yield return ToCategory(dbReader);
            }
        }

        private static CategoryDataRow ToCategory(IDataReader dbReader)
        {
            var idIndex = dbReader.GetOrdinal("ID");
            var titleIndex = dbReader.GetOrdinal("Title");
            var parentIdIndex = dbReader.GetOrdinal("ParentId");

            return new CategoryDataRow(
                dbReader.GetString(idIndex),
                dbReader.GetString(titleIndex),
                dbReader.GetStringOrNull(parentIdIndex));
        }
    }
}
