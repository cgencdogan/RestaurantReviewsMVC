using RestaurantReviews.Models.Entities;
using System.Collections.Generic;

namespace RestaurantReviews.Models.Contracts {
    public interface IRestaurantImageRepository : IBaseRepository<RestaurantImage> {
        void DeleteByRestaurantId(int restaurantId);
        void DeleteByUserId(string userId);
        List<RestaurantImage> GetByRestaurantId(int restaurantId);
    }
}
