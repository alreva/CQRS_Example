using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site.Controllers
{
    public class AddCategoryModel
    {
        public AddCategoryModel() : this(new SelectListItem[0])
        {
        }

        public AddCategoryModel(params SelectListItem[] availableCategoryModels)
        {
            AvailableCategoryModels = availableCategoryModels;
        }

        public IEnumerable<SelectListItem> AvailableCategoryModels { get; private set; }
        public string Title { get; set; }
        public string ParentId { get; set; }
    }
}
