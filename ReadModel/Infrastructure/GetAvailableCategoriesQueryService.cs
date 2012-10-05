using System.Collections.Generic;
using CQRS;

namespace ReadModel.Infrastructure
{
    public class GetAvailableCategoriesQueryService : AdoNetQueryService, IGetAvailableCategoriesQueryService
    {
        public GetAvailableCategoriesQueryService(IAdoNetConnectionProvider connectionProvider)
            : base(connectionProvider)
        {
        }

        public IEnumerable<AvailableCategoryDto> GetAvailableCategories()
        {
            return ExecuteDirectTableReaderEnumerable(
                "AvailableCategories",
                QueryServiceHelper.ToCategory);
        }


    }
}
