using RestaurantReviews.BLL.Service;
using RestaurantReviews.Models.Entities;
using RestaurantReviews.WebUI.Models;
using RestaurantReviews.WebUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RestaurantReviews.WebUI.Controllers {
    public class RestaurantController : Controller {
        DataService service;
        public RestaurantController() {
            service = new DataService();
        }

        public ActionResult Detail(int id) {
            var model = new RestaurantDetailVm();
            model.Restaurant = service.Uow.Restaurants.GetByIdIncludeDistrict(id);
            model.RestaurantCategories = service.Uow.RestaurantCategories.GetCategoriesByRestaurantId(id);
            model.RestaurantFeatures = service.Uow.RestaurantFeatures.GetNamesByRestaurantId(id);
            model.Score = service.ReviewManager.CalculateScore(service.Uow.Reviews, id);
            model.ReviewCount = service.Uow.Reviews.GetCountByRestaurantId(id);
            return View(model);
        }

        public ActionResult ReviewList(int restaurantId, int pageNumber = 0) {
            var model = new ReviewListVm();
            var reviewCount = service.Uow.Reviews.GetCountByRestaurantId(restaurantId);
            var reviews = service.Uow.Reviews.GetByRestaurantIdIncludeUserTakeX(restaurantId, pageNumber, PageUtil.ReviewListShownCount);
            model.Reviews = new List<ReviewVm>();
            foreach (var review in reviews) {
                var reviewVm = new ReviewVm();
                reviewVm.Id = review.Id;
                reviewVm.Username = review.AppUser.UserName;
                reviewVm.Score = review.Score;
                reviewVm.UserReviewCount = service.Uow.Reviews.GetCountByUserId(review.UserId);
                reviewVm.Content = review.Content;
                reviewVm.ReviewDate = review.AddedDate;
                reviewVm.ProfilePicPath = review.AppUser.ProfilePicPath;
                model.Reviews.Add(reviewVm);
            }
            model.RestaurantId = restaurantId;
            var maxPage = Math.Ceiling(reviewCount / Convert.ToDouble(PageUtil.ReviewListShownCount));
            model.ReviewCount = reviewCount;
            model.PageNumber = pageNumber;
            model.MaxPage = maxPage;
            ViewBag.pageNumber = pageNumber;
            ViewBag.restaurantId = restaurantId;
            ViewBag.maxPage = maxPage;
            return PartialView(model);
        }

        public ActionResult ImageList(int restaurantId) {
            var model = new ImageListVm();
            var images = service.Uow.RestaurantImages.GetByRestaurantId(restaurantId);
            if (images.Count > 0) {
                model.RestaurantImages = new List<RestaurantImage>();
                model.RestaurantImages.AddRange(images);
            }
            model.RestaurantId = restaurantId;
            return PartialView(model);
        }

        public ActionResult SuggestionList(int restaurantId) {
            var model = new SugestionListVm();
            List<int> restaurantIds = new List<int>();
            List<int> restaurantIdsByCategory = new List<int>();
            var restaurant = service.Uow.Restaurants.GetById(restaurantId);

            var restaurantsByDistrictOrderedByScore = service.Uow.Reviews.FilterByAllTakeX(PageUtil.SuggestionRestaurantShownCount, r => r.Restaurant.DistrictId == restaurant.DistrictId);
            foreach (var item in (dynamic)(restaurantsByDistrictOrderedByScore)) {
                restaurantIds.Add(item.GetType().GetProperty("ID").GetValue(item));
            }
            restaurantIds.Remove(restaurantId);
            var restaurantsInSameDistrict = service.Uow.Restaurants.GetByRestaurantIds(restaurantIds);
            model.RestaurantsInSameDistrict = new List<RestaurantListVm>();

            foreach (var item in restaurantsInSameDistrict) {
                var rest = new RestaurantListVm();
                rest.Id = item.Id;
                rest.Name = item.Name;
                rest.PicturePath = item.CoverImagePath;
                rest.Score = service.ReviewManager.CalculateScore(service.Uow.Reviews, item.Id);
                model.RestaurantsInSameDistrict.Add(rest);
            }

            var categoryIds = service.Uow.RestaurantCategories.GetByRestaurantId(restaurantId);

            model.RestaurantsInSameCategories = new List<List<RestaurantListVm>>();

            foreach (var categoryId in categoryIds) {
                restaurantIds.Clear();
                restaurantIdsByCategory.Clear();
                var restaurantsByCategoryId = service.Uow.RestaurantCategories.GetByCategoryId(categoryId).Select(x => new { x.RestaurantId });
                foreach (var item in restaurantsByCategoryId) {
                    restaurantIdsByCategory.Add(item.RestaurantId);
                }

                var restaurantsByCategoryOrderedByScore = service.Uow.Reviews.FilterByAllTakeX(PageUtil.SuggestionRestaurantShownCount, r => restaurantIdsByCategory.Contains(r.RestaurantId));
                foreach (var item in (dynamic)(restaurantsByCategoryOrderedByScore)) {
                    restaurantIds.Add(item.GetType().GetProperty("ID").GetValue(item));
                }

                restaurantIds.Remove(restaurantId);
                var restaurants = service.Uow.Restaurants.GetByRestaurantIdsTakeX(restaurantIds, PageUtil.SuggestionRestaurantShownCount);

                var restaurantsInSameCategory = new List<RestaurantListVm>();
                foreach (var item in restaurants) {
                    var rest = new RestaurantListVm();
                    rest.Id = item.Id;
                    rest.Name = item.Name;
                    rest.PicturePath = item.CoverImagePath;
                    rest.Score = service.ReviewManager.CalculateScore(service.Uow.Reviews, item.Id);
                    restaurantsInSameCategory.Add(rest);
                }
                model.RestaurantsInSameCategories.Add(restaurantsInSameCategory);
            }

            model.District = restaurant.District;
            model.Categories = service.Uow.Categories.GetByCategoryIds(categoryIds);
            return PartialView(model);
        }
    }
}