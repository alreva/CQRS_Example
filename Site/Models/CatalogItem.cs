using System.Collections.Generic;

namespace Site.Models
{
    public class CatalogItemModel
    {
        public CatalogItemModel(string id, string title)
        {
            Title = title;
            Id = id;
        }

        public string Id { get; private set; }
        public string Title { get; private set; }
    }
}