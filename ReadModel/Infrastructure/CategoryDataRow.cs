namespace ReadModel.Infrastructure
{
    internal class CategoryDataRow
    {
        public CategoryDataRow(string id, string title, string parentId)
        {
            Id = id;
            Title = title;
            ParentId = parentId;
        }

        public string Id { get; private set; }
        public string Title { get; private set; }
        public string ParentId { get; private set; }
    }
}