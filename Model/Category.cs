using System;
using CQRS;

namespace Model
{
    public class Category : AggregateRoot
    {
        public Category()
        {
            IsBrandNew = true;
        }

        public void AddNew(string id, string title, string parentId)
        {
            if (!IsBrandNew)
            {
                throw new InvalidOperationException("The object is not new. Cannot apply AddNew more than once.");
            }

            ApplyAndAddChange(new CategoryAdded(id, title, parentId));
        }

        private void Apply(CategoryAdded evt)
        {
            IsBrandNew = false;

            Id = evt.Id;
            Title = evt.Title;
            ParentId = evt.ParentId;
        }

        private bool IsBrandNew { get; set; }

        private string ParentId { get; set; }
        private string Title { get; set; }
    }
}