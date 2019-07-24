using RestaurantReviews.BLL.Service;
using RestaurantReviews.Models.Entities;
using RestaurantReviews.WebUI.Areas.Admin.Models;
using RestaurantReviews.WebUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RestaurantReviews.WebUI.Areas.Admin.Controllers {
    public class FeaturePanelController : Controller {
        DataService service;
        public FeaturePanelController() {
            service = new DataService();
        }
        public ActionResult List(string searchWord = "", int pageNumber = 0) {
            var model = new FeatureVm();
            var features = new List<Feature>();
            if (string.IsNullOrWhiteSpace(searchWord)) {
                features = service.Uow.Features.GetActives();
            }
            else {
                features = service.Uow.Features.GetBySearchWord(searchWord);
            }
            var maxPage = Math.Ceiling(features.Count / Convert.ToDouble(PageUtil.PanelFeatureShownCount));
            model.Features = features.OrderBy(o => o.Name).Skip(pageNumber * PageUtil.PanelFeatureShownCount).Take(PageUtil.PanelFeatureShownCount).ToList();
            model.SearchWord = searchWord;
            model.PageNumber = pageNumber;
            model.ShownCount = PageUtil.PanelFeatureShownCount;
            model.MaxPage = maxPage;
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Feature feature) {
            service.Uow.Features.Insert(feature);
            service.Uow.Save();
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Update(Feature feature) {
            var featureToBeUpdated = service.Uow.Features.GetById(feature.Id);
            featureToBeUpdated.Name = feature.Name;
            service.Uow.Features.Update(featureToBeUpdated);
            service.Uow.Save();
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Delete(int featureId) {
            var feature = service.Uow.Features.GetById(featureId);
            service.Uow.RestaurantFeatures.DeleteByFeatureId(featureId);
            service.Uow.Features.Delete(featureId);
            service.Uow.Save();
            return RedirectToAction("List");
        }
    }
}