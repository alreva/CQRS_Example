using System.Collections.Generic;
using System.Data;
using CQRS;

namespace ReadModel.Infrastructure
{
    public class GetAvailableCategoriesQueryService : AdoNetQueryService, IGetAvailableCategoriesQueryService
    {
        public GetAvailableCategoriesQueryService(IConnectionProvider connectionProvider)
            : base(connectionProvider)
        {
        }

        public IEnumerable<AvailableCategoryDto> GetAvailableCategories()
        {
            return ExecuteDirectTableReaderEnumerable(
                "AvailableCategories",
                ToCategory);
        }

        private static AvailableCategoryDto ToCategory(IDataReader dbReader)
        {
            var idIndex = dbReader.GetOrdinal("ID");
            var titleIndex = dbReader.GetOrdinal("Title");

            return new AvailableCategoryDto(
                dbReader.GetString(idIndex),
                dbReader.GetString(titleIndex));
        }
    }
}
