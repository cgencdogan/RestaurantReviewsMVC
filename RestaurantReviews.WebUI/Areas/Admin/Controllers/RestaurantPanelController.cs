using Microsoft.AspNet.Identity;
using RestaurantReviews.BLL.Service;
using RestaurantReviews.Models.Entities;
using RestaurantReviews.WebUI.Areas.Admin.Models;
using RestaurantReviews.WebUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;

namespace RestaurantReviews.WebUI.Areas.Admin.Controllers {
    [Authorize(Roles = "admin, moderator")]
    public class RestaurantPanelController : Controller {
        DataService service;
        public RestaurantPanelController() {
            service = new DataService();
        }
        public ActionResult Add() {
            var model = new AddRestaurantVm();
            model.Districts = service.Uow.Districts.GetAll();
            model.Features = service.Uow.Features.GetActives();
            model.Categories = service.Uow.Categories.GetAll();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(AddRestaurantVm data) {
            if (ModelState.IsValid) {
                var restaurant = new Restaurant();
                restaurant.RestaurantKey = Guid.NewGuid().ToString();
                restaurant.Name = data.RestaurantName;
                restaurant.Adress = data.Adress;
                restaurant.PhoneNumber = data.PhoneNumber;
                restaurant.DistrictId = data.DistrictId;
                restaurant.AddedBy = User.Identity.GetUserId();
                WebImage img = new WebImage(data.RestaurantImage.InputStream);
                img.Resize(640, 360, false);
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
                img.FileName = fileName + "." + img.ImageFormat;
                restaurant.CoverImagePath = "/Content/Images/CoverPics/" + img.FileName;
                img.Save(Server.MapPath("/Content/Images/CoverPics/") + img.FileName);
                service.Uow.Restaurants.Insert(restaurant);
                service.Uow.Save();

                int restaurantId = service.Uow.Restaurants.GetByRestaurantKey(restaurant.RestaurantKey).Id;
                if (data.FeatureIds != null) {
                    foreach (var featureId in data.FeatureIds) {
                        var restaurantFeature = new RestaurantFeature() {
                            RestaurantId = restaurantId,
                            FeatureId = featureId
                        };
                        service.Uow.RestaurantFeatures.Insert(restaurantFeature);
                    }
                }
                if (data.CategoryIds != null) {
                    foreach (var categoryId in data.CategoryIds) {
                        var restaurantCategory = new RestaurantCategory() {
                            RestaurantId = restaurantId,
                            CategoryId = categoryId
                        };
                        service.Uow.RestaurantCategories.Insert(restaurantCategory);
                    }
                }
                var dummyReview = new Review() {
                    Content = "Dummy Content",
                    Score = null,
                    UserId = User.Identity.GetUserId(),
                    RestaurantId = restaurantId,
                    isConfirmed = true,
                    AddedDate = DateTime.Now,
                    LastUpdatedDate = DateTime.Now,
                    IsActive = true
                };
                service.Uow.Reviews.Insert(dummyReview);
                service.Uow.Save();
            }
            return RedirectToAction("Add");
        }

        public ActionResult List(string searchWord, int pageNumber = 0) {
            var model = new PanelRestaurantListVm();
            IEnumerable<Restaurant> restaurants;
            int restaurantCount;
            if (searchWord != null) {
                restaurants = service.Uow.Restaurants.GetIncludeDistrictsFilterBySearchWordTakeX(searchWord, pageNumber, PageUtil.PanelRestaurantShownCount);
                restaurantCount = service.Uow.Restaurants.GetCountBySearchWord(searchWord);
            }
            else {
                restaurants = service.Uow.Restaurants.GetIncludeDistrictsTakeX(pageNumber, PageUtil.PanelRestaurantShownCount);
                restaurantCount = service.Uow.Restaurants.GetCount();
            }
            model.RestaurantList = new List<PanelRestaurantVm>();
            foreach (var restaurant in restaurants) {
                model.RestaurantList.Add(new PanelRestaurantVm {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    PhoneNumber = restaurant.PhoneNumber,
                    Adress = restaurant.Adress,
                    District = restaurant.District
                }
                    );
            }
            model.SearchWord = searchWord;
            var maxPage = Math.Ceiling(restaurantCount / Convert.ToDouble(PageUtil.PanelRestaurantShownCount));
            model.ShownCount = PageUtil.PanelRestaurantShownCount;
            model.PageNumber = pageNumber;
            model.MaxPage = maxPage;
            return View(model);
        }

        public ActionResult PassiveList(string searchWord, int pageNumber = 0) {
            var model = new PanelRestaurantListVm();
            IEnumerable<Restaurant> restaurants;
            int passiveRestaurantCount;
            if (searchWord != null) {
                restaurants = service.Uow.Restaurants.GetPassivesIncludeDistrictsFilterBySearchWordTakeX(searchWord, pageNumber, PageUtil.PanelRestaurantShownCount);
                passiveRestaurantCount = service.Uow.Restaurants.GetPassiveCountBySearchWord(searchWord);
            }
            else {
                restaurants = service.Uow.Restaurants.GetPassivesIncludeDistrictsTakeX(pageNumber, PageUtil.PanelRestaurantShownCount);
                passiveRestaurantCount = service.Uow.Restaurants.GetPassiveCount();
            }

            model.RestaurantList = new List<PanelRestaurantVm>();
            foreach (var restaurant in restaurants) {
                model.RestaurantList.Add(new PanelRestaurantVm {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    PhoneNumber = restaurant.PhoneNumber,
                    Adress = restaurant.Adress,
                    District = restaurant.District
                }
                    );
            }
            model.SearchWord = searchWord;
            passiveRestaurantCount = service.Uow.Restaurants.GetPassiveCount();
            var maxPage = Math.Ceiling(passiveRestaurantCount / Convert.ToDouble(PageUtil.PanelRestaurantShownCount));
            model.ShownCount = PageUtil.PanelRestaurantShownCount;
            model.PageNumber = pageNumber;
            model.MaxPage = maxPage;
            return View(model);
        }

        public ActionResult Update(int id) {
            var model = new UpdateRestaurantVm();
            model.RestaurantId = id;
            model.Districts = service.Uow.Districts.GetAll();
            model.Features = service.Uow.Features.GetActives();
            model.Categories = service.Uow.Categories.GetAll();
            var restaurant = service.Uow.Restaurants.GetById(id);
            model.RestaurantName = restaurant.Name;
            model.Adress = restaurant.Adress;
            model.PhoneNumber = restaurant.PhoneNumber;
            var categories = service.Uow.RestaurantCategories.GetByRestaurantIdIncludeCategories(id);
            model.CategoryIds = new List<int>();
            foreach (var category in categories) {
                model.CategoryIds.Add(category.CategoryId);
            }
            model.DistrictId = restaurant.DistrictId;
            var features = service.Uow.RestaurantFeatures.GetByRestaurantIdIncludeFeatures(id);
            model.FeatureIds = new List<int>();
            foreach (var feature in features) {
                model.FeatureIds.Add(feature.FeatureId);
            }
            model.CoverImagePath = restaurant.CoverImagePath;
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(UpdateRestaurantVm data) {
            var restaurant = service.Uow.Restaurants.GetById(data.RestaurantId);
            restaurant.Name = data.RestaurantName;
            restaurant.Adress = data.Adress;
            restaurant.PhoneNumber = data.PhoneNumber;
            restaurant.DistrictId = data.DistrictId;
            restaurant.LastUpdatedDate = DateTime.Now;

            if (data.RestaurantImage != null) {
                if (System.IO.File.Exists(Server.MapPath(restaurant.CoverImagePath))) {
                    System.IO.File.Delete(Server.MapPath(restaurant.CoverImagePath));
                }

                WebImage img = new WebImage(data.RestaurantImage.InputStream);
                img.Resize(640, 360, false);
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
                img.FileName = fileName + "." + img.ImageFormat;
                restaurant.CoverImagePath = "/Content/Images/CoverPics/" + img.FileName;
                img.Save(Server.MapPath("/Content/Images/CoverPics/") + img.FileName);
            }

            service.Uow.Restaurants.Update(restaurant);

            if (data.FeatureIds != null) {
                service.Uow.RestaurantFeatures.DeleteByRestaurantId(data.RestaurantId);
                foreach (var featureId in data.FeatureIds) {
                    var restaurantFeature = new RestaurantFeature() {
                        RestaurantId = data.RestaurantId,
                        FeatureId = featureId
                    };
                    service.Uow.RestaurantFeatures.Insert(restaurantFeature);
                }
            }

            if (data.CategoryIds != null) {
                service.Uow.RestaurantCategories.DeleteByRestaurantId(data.RestaurantId);
                foreach (var categoryId in data.CategoryIds) {
                    var restaurantCategory = new RestaurantCategory() {
                        RestaurantId = data.RestaurantId,
                        CategoryId = categoryId
                    };
                    service.Uow.RestaurantCategories.Insert(restaurantCategory);
                }
            }
            service.Uow.Save();
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Delete(int id) {
            var restaurant = service.Uow.Restaurants.GetById(id);
            restaurant.IsActive = false;
            service.Uow.Restaurants.Update(restaurant);
            service.Uow.Save();
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Activate(int id) {
            var restaurantToBeActivated = service.Uow.Restaurants.GetById(id);
            restaurantToBeActivated.IsActive = true;
            service.Uow.Restaurants.Update(restaurantToBeActivated);
            service.Uow.Save();
            return RedirectToAction("PassiveList");
        }

        [HttpPost]
        public ActionResult PermDelete(int id) {
            var restaurantToBeDeleted = service.Uow.Restaurants.GetById(id);
            if (System.IO.File.Exists(Server.MapPath(restaurantToBeDeleted.CoverImagePath))) {
                System.IO.File.Delete(Server.MapPath(restaurantToBeDeleted.CoverImagePath));
            }

            var restaurantImages = service.Uow.RestaurantImages.Get(i => i.RestaurantId == id).Select(i => i.ImgFilePath).ToList();
            foreach (var imagePath in restaurantImages) {
                if (System.IO.File.Exists(Server.MapPath(imagePath))) {
                    System.IO.File.Delete(Server.MapPath(imagePath));
                }
            }
            service.Uow.RestaurantFeatures.DeleteByRestaurantId(id);
            service.Uow.RestaurantCategories.DeleteByRestaurantId(id);
            service.Uow.RestaurantImages.DeleteByRestaurantId(id);
            service.Uow.Restaurants.Delete(id);
            service.Uow.Save();
            return RedirectToAction("PassiveList");
        }
    }
}