using Microsoft.AspNet.Identity;
using RestaurantReviews.BLL.Managers;
using RestaurantReviews.BLL.Service;
using RestaurantReviews.Models.Entities;
using RestaurantReviews.Models.Entities.Identity;
using RestaurantReviews.WebUI.Models;
using RestaurantReviews.WebUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace RestaurantReviews.WebUI.Controllers
{
    public class HomeController : Controller
    {
        DataService service;
        DummyManager dummyManager;
        public HomeController()
        {
            service = new DataService();
            dummyManager = new DummyManager();
        }
        public ActionResult Index(string searchWord = "", int pageNumber = 0, int districtId = 0, int categoryId = 0)
        {
            #region CreateAdmin
            //service.Uow.Roles.Create(new AppRole { Name = "default_user" });
            //service.Uow.Roles.Create(new AppRole { Name = "admin" });
            //service.Uow.Roles.Create(new AppRole { Name = "moderator" });
            //service.Uow.Users.Create(new AppUser
            //{
            //    Email = "admin@admin.com",
            //    UserName = "admin",
            //    ProfilePicPath = "/Content/Images/ProfilePics/default-pp.jpg",
            //}, "qwe123");
            //var userId = service.Uow.Users.FindByName("admin").Id;
            //service.Uow.Users.AddToRole(userId, "admin");
            #endregion
            //dummyManager.CreateDummyRestaurants(134, 3, 15);
            List<int> restaurantIds = new List<int>();
            List<int> restaurantIdsForPage = new List<int>();
            List<int> restaurantIdsByCategory = new List<int>();
            string districtName = null, categoryName = null;

            var model = new RestaurantVm();
            model.Districts = service.Uow.Districts.GetAll();
            model.Categories = service.Uow.Categories.GetAll();
            model.District = null;
            model.Category = null;
            model.RestaurantListVm = new List<RestaurantListVm>();
            var restaurants = new List<Restaurant>();
            Expression<Func<Review, bool>> expression = null;
            if (!(searchWord == ""))
            {
                expression = (r => r.Restaurant.Name.Contains(searchWord));
            }
            if (districtId != 0)
            {
                expression = (r => r.Restaurant.DistrictId == districtId);
                model.District = service.Uow.Districts.GetById(districtId);
                districtName = model.District.Name;
            }

            var filteredRestaurants = service.Uow.Reviews.FilterByAll(expression);

            foreach (var item in (dynamic)(filteredRestaurants))
            {
                restaurantIds.Add(item.GetType().GetProperty("ID").GetValue(item));
            }

            if (categoryId != 0)
            {
                model.Category = service.Uow.Categories.GetById(categoryId);
                categoryName = model.Category.Name;
                var restaurantsByCategoryId = service.Uow.RestaurantCategories.GetByCategoryId(categoryId);
                foreach (var item in restaurantsByCategoryId)
                {
                    restaurantIdsByCategory.Add(item.RestaurantId);
                }
                restaurantIds = restaurantIds.Intersect(restaurantIdsByCategory).ToList();
            }
            restaurantIdsForPage = restaurantIds.Skip(pageNumber * PageUtil.HomeRestaurantShownCount).Take(PageUtil.HomeRestaurantShownCount).ToList();
            restaurants = service.Uow.Restaurants.GetByRestaurantIdsIncludeDistricts(restaurantIdsForPage).ToList();

            foreach (var restaurant in restaurants)
            {
                model.RestaurantListVm.Add(new RestaurantListVm
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    PicturePath = restaurant.CoverImagePath,
                    District = restaurant.District,
                    Score = service.ReviewManager.CalculateScore(service.Uow.Reviews, restaurant.Id),
                    ReviewCount = service.Uow.Reviews.GetCountByRestaurantId(restaurant.Id)
                });
            }

            model.RestaurantListVm = model.RestaurantListVm.OrderByDescending(o => o.Score).ToList();

            var maxPage = Math.Ceiling(restaurantIds.Count() / Convert.ToDouble(PageUtil.HomeRestaurantShownCount));
            model.SearchWord = searchWord;
            model.MaxPage = maxPage;
            return View(model);
        }

        public ActionResult SearchByCategory(string searchWord)
        {
            var categoryId = service.Uow.Categories.GetIdBySearchWord(searchWord);
            return Json(new { categoryId = categoryId });
        }

        public ActionResult SearchByName(string searchWord)
        {
            var restaurants = service.Uow.Restaurants.GetBySearchWord(searchWord);
            return View();
        }

        public ActionResult SearchByDistrict(string searchWord)
        {
            var districtId = service.Uow.Districts.GetIdBySearchWord(searchWord);
            return Json(new { districtId = districtId });
        }
    }
}