using RestaurantReviews.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RestaurantReviews.Models.Contracts {
    public interface IRestaurantCategoryRepository : IBaseRepository<RestaurantCategory> {
        void DeleteByRestaurantId(int restaurantId);
        void DeleteByCategoryId(int categoryId);
        int GetCountByCategoryId(int categoryId);
        List<int> GetByRestaurantId(int restaurantId);
        List<Category> GetCategoriesByRestaurantId(int restaurantId);
        List<RestaurantCategory> GetIncludeCategories(Expression<Func<RestaurantCategory, bool>> predicate);
        List<RestaurantCategory> GetByRestaurantIdIncludeCategories(int restaurantId);
        List<RestaurantCategory> GetByCategoryId(int categoryId);
    }
}
