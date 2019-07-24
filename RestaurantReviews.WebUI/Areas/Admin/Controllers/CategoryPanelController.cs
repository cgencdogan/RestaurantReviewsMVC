using RestaurantReviews.BLL.Service;
using RestaurantReviews.Models.Entities;
using RestaurantReviews.WebUI.Areas.Admin.Models;
using RestaurantReviews.WebUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RestaurantReviews.WebUI.Areas.Admin.Controllers {
    public class CategoryPanelController : Controller {
        DataService service;
        public CategoryPanelController() {
            service = new DataService();
        }

        public ActionResult List(string searchWord = "", int pageNumber = 0) {
            var categories = new List<Category>();
            if (string.IsNullOrWhiteSpace(searchWord)) {
                categories = service.Uow.Categories.GetAll();
            }
            else {
                categories = service.Uow.Categories.Get(c => c.Name.Contains(searchWord));
            }
            var model = new CategoryListVm();
            model.Categories = new List<CategoryVm>();
            foreach (var item in categories) {
                model.Categories.Add(new CategoryVm { Category = item, Count = service.Uow.RestaurantCategories.GetCountByCategoryId(item.Id) });
            }
            var maxPage = Math.Ceiling(model.Categories.Count / Convert.ToDouble(PageUtil.CategoryShownCount));
            model.Categories = model.Categories.OrderByDescending(m => m.Count).Skip(pageNumber * PageUtil.CategoryShownCount).Take(PageUtil.CategoryShownCount).ToList();
            model.SearchWord = searchWord;
            model.PageNumber = pageNumber;
            model.MaxPage = maxPage;
            model.ShownAmount = PageUtil.CategoryShownCount;
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Category category) {
            service.Uow.Categories.Insert(category);
            service.Uow.Save();
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Update(CategoryUpdateVm category) {
            var categoryToBeUpdated = service.Uow.Categories.GetById(category.Id);
            categoryToBeUpdated.Name = category.Name;
            service.Uow.Categories.Update(categoryToBeUpdated);
            service.Uow.Save();
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Delete(int categoryId) {
            var category = service.Uow.Categories.GetById(categoryId);
            service.Uow.RestaurantCategories.DeleteByCategoryId(categoryId);
            service.Uow.Categories.Delete(categoryId);
            service.Uow.Save();
            return RedirectToAction("List");
        }
    }
}