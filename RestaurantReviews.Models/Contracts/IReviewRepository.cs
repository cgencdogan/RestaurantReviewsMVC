using RestaurantReviews.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RestaurantReviews.Models.Contracts {
    public interface IReviewRepository : IBaseRepository<Review> {
        void DeleteByUserId(string userId);
        int GetCount();
        int GetConfirmedCount();
        int GetUnconfirmedCount();
        int GetCountByUserId(string userId);
        int GetCountByRestaurantId(int id);
        object FilterByAll(Expression<Func<Review, bool>> expression);
        object FilterByAllTakeX(int takeCount, Expression<Func<Review, bool>> expression);
        List<Review> GetIncludeUser(Expression<Func<Review, bool>> predicate);
        List<Review> GetIncludeRestaurant(Expression<Func<Review, bool>> predicate);
        List<Review> GetByRestaurantIdIncludeUsers(int restaurantId, int rowCount = 0, int? pageNumber = 0);
        List<Review> GetConfirmedIncludeUserTakeX(int pageNumber, int shownAmount);
        List<Review> GetUnconfirmedIncludeUserTakeX(int pageNumber, int shownAmount);
        List<Review> GetByUserIdIncludeRestaurantTakeX(string userId, int pageNumber, int shownAmount);
        List<Review> GetByRestaurantIdIncludeUserTakeX(int restaurantId, int pageNumber, int shownAmount);
        string GetCountAllMonths();
    }
}
