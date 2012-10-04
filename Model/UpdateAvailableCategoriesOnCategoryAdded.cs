using System.Data;
using CQRS;

namespace Model
{
    public class UpdateAvailableCategoriesOnCategoryAdded : AdoNetEventHandler<CategoryAdded>
    {
        public UpdateAvailableCategoriesOnCategoryAdded(IConnectionProvider connectionProvider) : base(connectionProvider)
        {
        }

        protected override void Handle(CategoryAdded evt, IDbCommand cmd)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO AvailableCategories ([Id],[Title]) values (@id, @title)";
            cmd.AddStringParameter("id", evt.Id);
            cmd.AddStringParameter("title", evt.Title, 255);

            cmd.ExecuteNonQuery();
        }
    }
}