using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site.Models
{
    public class IndexModel
    {
        public IndexModel(CategoryModel catalogTree, ProductsListModel products)
        {
            Products = products;
            CatalogTree = catalogTree;
        }

        public CategoryModel CatalogTree { get; private set; }

        public ProductsListModel Products { get; private set; }
    }
}