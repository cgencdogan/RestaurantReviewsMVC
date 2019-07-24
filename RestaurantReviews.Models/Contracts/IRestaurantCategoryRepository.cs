using RestaurantReviews.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RestaurantReviews.Models.Contracts {
    public interface IRestaurantCategoryRepository : IBaseRepository<RestaurantCategory> {
        List<RestaurantCategory> GetIncludeCategories(Expression<Func<RestaurantCategory, bool>> predicate);
        void DeleteByRestaurantId(int restaurantId);
        List<int> GetByRestaurantId(int restaurantId);
        List<RestaurantCategory> GetByRestaurantIdIncludeCategories(int restaurantId);
        List<RestaurantCategory> GetByCategoryId(int categoryId);
        int GetCountByCategoryId(int categoryId);
        void DeleteByCategoryId(int categoryId);
    }
}
