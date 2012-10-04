using CQRS;

namespace Model
{
    public class CategoryAdded : Event
    {
        public CategoryAdded(string id, string title, string parentId)
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