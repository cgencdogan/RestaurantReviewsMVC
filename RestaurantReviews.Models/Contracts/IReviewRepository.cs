using RestaurantReviews.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RestaurantReviews.Models.Contracts {
    public interface IReviewRepository : IBaseRepository<Review> {
        List<Review> GetByRestaurantIdIncludeUsers(int restaurantId, int rowCount = 0, int? pageNumber = 0);
        List<Review> GetIncludeRestaurant(Expression<Func<Review, bool>> predicate);
        List<Review> GetAllIncludeUser(Expression<Func<Review, bool>> predicate);
        object FilterByAll(Expression<Func<Review, bool>> expression);
        object FilterByAllTakeX(int takeCount, Expression<Func<Review, bool>> expression);
        void DeleteByUserId(string userId);
        int GetCountByUserId(string userId);
        int GetCountByRestaurantId(int id);
        int GetCount();
        int GetConfirmedCount();
        int GetUnconfirmedCount();
        List<Review> GetAllConfirmedIncludeUserTakeX(int pageNumber, int shownAmount);
        List<Review> GetAllUnconfirmedIncludeUserTakeX(int pageNumber, int shownAmount);
        List<Review> GetByUserIdIncludeRestaurantTakeX(string userId, int pageNumber, int shownAmount);
        List<Review> GetByRestaurantIdIncludeUserTakeX(int restaurantId, int pageNumber, int shownAmount);
    }
}
