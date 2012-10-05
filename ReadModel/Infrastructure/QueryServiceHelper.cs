using System.Data;

namespace ReadModel.Infrastructure
{
    public static class QueryServiceHelper
    {
        public static AvailableCategoryDto ToCategory(IDataReader dbReader)
        {
            var idIndex = dbReader.GetOrdinal("ID");
            var titleIndex = dbReader.GetOrdinal("Title");

            return new AvailableCategoryDto(
                dbReader.GetString(idIndex),
                dbReader.GetString(titleIndex));
        }
    }
}