namespace ReadModel
{
    public interface IGetAvailableCategoryByIdQueryService
    {
        AvailableCategoryDto GetAvailableCategoryById(string id);
    }
}