using RestaurantReviews.Models.Contracts;
using RestaurantReviews.Models.Entities;
using System.Collections.Generic;

namespace RestaurantReviews.BLL.Managers {
    public class RestaurantManager {
        public List<Category> RestaurantCategories(IRestaurantCategoryRepository restaurantCategoryRepository, int id) {
            var categories = restaurantCategoryRepository.GetByRestaurantIdIncludeCategories(id);
            var categoryNames = new List<Category>();
            foreach (var item in categories) {
                categoryNames.Add(item.Category);
            }
            return categoryNames;
        }

        public List<string> RestaurantFeatures(IRestaurantFeatureRepository restaurantFeatureRepository, int id) {
            var features = restaurantFeatureRepository.GetByRestaurantIdIncludeFeatures(id);
            var featureNames = new List<string>();
            foreach (var item in features) {
                featureNames.Add(item.Feature.Name);
            }
            return featureNames;
        }
    }
}
