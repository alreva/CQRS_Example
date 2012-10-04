using System.Collections.Generic;

namespace Site.Models
{
    public class CategoryModel : CatalogItemModel
    {
        public CategoryModel(string id, string title, bool selected, params CategoryModel[] items)
            : base(id, title)
        {
            Selected = selected;
            Items = items;
        }

        public bool Selected { get; private set; }
        public IEnumerable<CategoryModel> Items { get; private set; }
    }
}