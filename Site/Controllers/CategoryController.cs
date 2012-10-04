using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CQRS;
using Model;
using ReadModel;

namespace Site.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICommandSender _commandSender;
        private readonly IGetAvailableCategoriesQueryService _getAvailableCategoriesQueryService;

        public CategoryController(
            ICommandSender commandSender,
            IGetAvailableCategoriesQueryService getAvailableCategoriesQueryService)
        {
            _commandSender = commandSender;
            _getAvailableCategoriesQueryService = getAvailableCategoriesQueryService;
        }

        [HttpGet]
        public ActionResult Add()
        {
            var availableCategoryDtos = _getAvailableCategoriesQueryService.GetAvailableCategories();
            var availableCategoryModels = availableCategoryDtos
                .Select(dto => ToViewModel(dto))
                .AddEmpty();
            return View(new AddCategoryModel(availableCategoryModels.ToArray()));
        }

        private SelectListItem ToViewModel(AvailableCategoryDto dto)
        {
            return new SelectListItem
                {
                    Value = dto.Id,
                    Text = dto.Title,
                };
        }

        [HttpPost]
        public ActionResult Add(AddCategoryModel model)
        {
            // TODO: validate

            var catId = Guid.NewGuid().ToString();

            _commandSender.Send(new AddCategory(catId, model.Title, model.ParentId));

            return RedirectToAction(null);
        }
    }

    public static class SelectListExtensions
    {
        public static IEnumerable<SelectListItem> AddEmpty(this IEnumerable<SelectListItem> selectList, string value = null, string text = "")
        {
            return new[]
                {
                    new SelectListItem {Value = value, Text = text}
                }.Union(selectList);
        } 
    }
}