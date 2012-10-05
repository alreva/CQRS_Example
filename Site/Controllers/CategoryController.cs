using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CQRS;
using Model;
using ReadModel;
using Site.Models;

namespace Site.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICommandSender _commandSender;
        private readonly IGetAvailableCategoriesQueryService _getAvailableCategoriesQueryService;
        private readonly IGetAvailableCategoryByIdQueryService _getAvailableCategoryByIdQueryService;

        public CategoryController(
            ICommandSender commandSender,
            IGetAvailableCategoriesQueryService getAvailableCategoriesQueryService,
            IGetAvailableCategoryByIdQueryService getAvailableCategoryByIdQueryService)
        {
            _commandSender = commandSender;
            _getAvailableCategoriesQueryService = getAvailableCategoriesQueryService;
            _getAvailableCategoryByIdQueryService = getAvailableCategoryByIdQueryService;
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

        [HttpGet]
        public ActionResult ChangeTitle(string id)
        {
            var avilableCategoryDto = _getAvailableCategoryByIdQueryService.GetAvailableCategoryById(id);
            return View(new ChangeCategoryTitleModel{ Id = avilableCategoryDto.Id, Title = avilableCategoryDto.Title});
        }

        [HttpPost]
        public ActionResult ChangeTitle(ChangeCategoryTitleModel model)
        {
            //_commandSender.Send(new ChangeCategoryTitle(model.Id, model.Title));
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