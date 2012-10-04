using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CQRS;
using ReadModel;
using Site.Models;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGetCatalogTreeQueryService _catalogTreeQueryService;

        public HomeController(
            ICommandSender commandSender,
            IGetCatalogTreeQueryService catalogTreeQueryService)
        {
            _catalogTreeQueryService = catalogTreeQueryService;
        }

        public ActionResult Index(string id)
        {
            CategoryDto rootCategoryDto = _catalogTreeQueryService.GetCatalogTree();

            var categoryTree = ToViewModel(rootCategoryDto, id);

            return View(
                new IndexModel(
                    categoryTree,
                    new ProductsListModel()));
        }

        private static CategoryModel ToViewModel(CategoryDto categoryDto, string currentCategoryId)
        {
            return new CategoryModel(
                categoryDto.Id,
                categoryDto.Title,
                currentCategoryId == categoryDto.Id,
                categoryDto.SubCategories.Select(subCategory => ToViewModel(subCategory, currentCategoryId)).ToArray());
        }
    }
}
