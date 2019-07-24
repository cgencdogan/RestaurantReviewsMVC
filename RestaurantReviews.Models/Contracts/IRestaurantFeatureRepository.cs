using RestaurantReviews.Models.Entities;
using System.Collections.Generic;

namespace RestaurantReviews.Models.Contracts {
    public interface IRestaurantFeatureRepository : IBaseRepository<RestaurantFeature> {
        List<RestaurantFeature> GetByRestaurantIdIncludeFeatures(int id);
        void DeleteByRestaurantId(int restaurantId);
        void DeleteByFeatureId(int featureId);
    }
}
