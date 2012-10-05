using CQRS;

namespace ReadModel.Infrastructure
{
    public class GetAvailableCategoryByIdQueryService : AdoNetQueryService, IGetAvailableCategoryByIdQueryService
    {
        public GetAvailableCategoryByIdQueryService(IAdoNetConnectionProvider connectionProvider)
            : base(connectionProvider)
        {
        }

        public AvailableCategoryDto GetAvailableCategoryById(string id)
        {
            return ExecuteRecordByIdReader("AvailableCategories", id, QueryServiceHelper.ToCategory);
        }
    }
}
