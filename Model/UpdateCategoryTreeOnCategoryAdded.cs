using System.Data;
using CQRS;

namespace Model
{
    public class UpdateCategoryTreeOnCategoryAdded : AdoNetEventHandler<CategoryAdded>
    {
        public UpdateCategoryTreeOnCategoryAdded(IConnectionProvider connectionProvider) : base(connectionProvider)
        {
        }

        protected override void Handle(CategoryAdded evt, IDbCommand cmd)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO CatalogTree ([Id],[Title],[ParentId]) values (@id, @title, @parentId)";
            cmd.AddStringParameter("id", evt.Id);
            cmd.AddStringParameter("title", evt.Title, 255);
            cmd.AddStringParameter("parentId", evt.ParentId);

            cmd.ExecuteNonQuery();
        }
    }
}