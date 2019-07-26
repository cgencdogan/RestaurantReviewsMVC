﻿using RestaurantReviews.Models.Entities;
using System.Collections.Generic;

namespace RestaurantReviews.Models.Contracts {
    public interface IRestaurantFeatureRepository : IBaseRepository<RestaurantFeature> {
        void DeleteByRestaurantId(int restaurantId);
        void DeleteByFeatureId(int featureId);
        List<RestaurantFeature> GetByRestaurantIdIncludeFeatures(int id);
    }
}
