using System.Collections.Generic;

namespace ReadModel
{
    public interface IGetAvailableCategoriesQueryService
    {
        IEnumerable<AvailableCategoryDto> GetAvailableCategories();
    }
}