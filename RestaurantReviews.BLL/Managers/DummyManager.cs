using Microsoft.AspNet.Identity;
using RestaurantReviews.DAL.Context;
using RestaurantReviews.DAL.Repositories.UnitOfWork;
using RestaurantReviews.Models.Contracts;
using RestaurantReviews.Models.Entities;
using RestaurantReviews.Models.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.BLL.Managers
{
    public class DummyManager
    {
        private AppDbContext context;
        private IUnitOfWork _unitOfWork;

        public DummyManager()
        {
            context = new AppDbContext();
        }

        public IUnitOfWork Uow
        {
            get
            {
                if (_unitOfWork == null)
                    _unitOfWork = new UnitOfWork(context);
                return _unitOfWork;
            }
        }

        public void CreatDummyUsers(int userCount)
        {
            for (int i = 0; i < userCount; i++)
            {
                Uow.Users.Create(new AppUser
                {
                    Email = "test" + i + "@test.com",
                    UserName = "dummy" + i,
                    ProfilePicPath = "/Content/Images/ProfilePics/default-pp.jpg",
                }, "qwe123");
                var userId = Uow.Users.FindByName("dummy" + i).Id;
                Uow.Users.AddToRole(userId, "default_user");
            }
        }

        public void CreateDummyRestaurants(int amount, int minReviewCount, int maxReviewCount)
        {
            var districtIds = Uow.Districts.GetAll().Select(d => d.Id).ToList();
            var featureIds = Uow.Features.GetAll().Select(f => f.Id).ToList();
            var categoryIds = Uow.Categories.GetAll().Select(c => c.Id).ToList();
            var userIds = Uow.Users.Users.Select(u => u.Id).ToList();

            List<string> imagePaths = new List<string>();
            imagePaths.Add("/Content/Images/CoverPics/default-restaurant-cover.jpeg");
            imagePaths.Add("/Content/Images/CoverPics/default-restaurant-cover1.jpeg");
            imagePaths.Add("/Content/Images/CoverPics/default-restaurant-cover2.jpeg");
            imagePaths.Add("/Content/Images/CoverPics/default-restaurant-cover3.jpeg");
            imagePaths.Add("/Content/Images/CoverPics/default-restaurant-cover4.jpeg");

            Random r = new Random();
            for (int i = 1; i <= amount; i++)
            {
                var restaurant = new Restaurant();
                restaurant.RestaurantKey = Guid.NewGuid().ToString();
                restaurant.Name = "Deneme Restoran " + i;
                restaurant.Adress = "Test mah. Deneme sok. No:" + i;
                restaurant.PhoneNumber = "0005554488";
                restaurant.DistrictId = districtIds[r.Next(0, districtIds.Count())];
                restaurant.AddedBy = Uow.Users.FindByName("admin").Id;
                restaurant.CoverImagePath = imagePaths[r.Next(0, imagePaths.Count())];

                Uow.Restaurants.Insert(restaurant);
                Uow.Save();
                int restaurantId = Uow.Restaurants.GetByRestaurantKey(restaurant.RestaurantKey).Id;


                var restaurantCategory = new RestaurantCategory()
                {
                    RestaurantId = restaurantId,
                    CategoryId = categoryIds[r.Next(0, categoryIds.Count())]
                };
                Uow.RestaurantCategories.Insert(restaurantCategory);

                var startingReview = new Review()
                {
                    Content = "Dummy Content",
                    Score = null,
                    UserId = restaurant.AddedBy,
                    RestaurantId = restaurantId,
                    isConfirmed = true,
                    AddedDate = DateTime.Now,
                    LastUpdatedDate = DateTime.Now,
                    IsActive = true
                };
                Uow.Reviews.Insert(startingReview);

                AddDummyFeatures(restaurantId, r.Next(0, featureIds.Count()), featureIds);
                CreateDummyReviews(restaurantId, minReviewCount, maxReviewCount, userIds);
            }
            Uow.Save();
        }

        public void AddDummyFeatures(int restaurantId, int toBeAddedFeatureCount, List<int> featureIds)
        {
            List<int> addedFeatureIds = new List<int>();
            Random r = new Random();
            for (int j = 0; j < toBeAddedFeatureCount; j++)
            {
                var featureId = 0;
                do
                {
                    featureId = featureIds[r.Next(0, featureIds.Count())];
                } while (addedFeatureIds.Contains(featureId));

                var restaurantFeature = new RestaurantFeature()
                {
                    RestaurantId = restaurantId,
                    FeatureId = featureId
                };
                addedFeatureIds.Add(featureId);
                Uow.RestaurantFeatures.Insert(restaurantFeature);
            }
        }

        public void CreateDummyReviews(int restaurantId, int minReviewCount, int maxReviewCount, List<string> userIds)
        {
            List<string> loremReviews = new List<string>();
            loremReviews.Add("Lorem ipsum dolor sit amet, consectetur adipiscing elit.");
            loremReviews.Add("Praesent sit amet consectetur orci, a laoreet sapien.");
            loremReviews.Add("Nam pulvinar sem quis ante convallis, eget ultricies lacus placerat. Quisque feugiat felis massa, vitae bibendum magna tincidunt et.");
            loremReviews.Add("Cras porta, metus sed consectetur elementum, felis orci rutrum magna, ac efficitur magna lectus sed magna.");

            Random r = new Random();
            var reviewCount = r.Next(minReviewCount, maxReviewCount + 1);
            for (int j = 0; j < reviewCount; j++)
            {
                var dummyReview = new Review()
                {
                    Content = loremReviews[r.Next(0, loremReviews.Count())],
                    Score = (byte)(r.Next(1, 6)),
                    UserId = userIds[r.Next(0, userIds.Count)],
                    RestaurantId = restaurantId,
                    isConfirmed = true,
                    AddedDate = DateTime.Now,
                    LastUpdatedDate = DateTime.Now,
                    IsActive = true
                };
                Uow.Reviews.Insert(dummyReview);
            }
            Uow.Save();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
