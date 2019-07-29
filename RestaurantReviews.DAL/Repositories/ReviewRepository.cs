using RestaurantReviews.DAL.Context;
using RestaurantReviews.Models.Contracts;
using RestaurantReviews.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RestaurantReviews.DAL.Repositories {
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository {
        public ReviewRepository(AppDbContext context) : base(context) {
        }

        public List<Review> GetByRestaurantIdIncludeUsers(int restaurantId, int rowCount = 0, int? pageNumber = 0) {
            if (pageNumber == null) {
                return dbSet.Include("AppUser").Where(r => r.RestaurantId == restaurantId && r.IsActive && r.isConfirmed).Take(rowCount).ToList();
            }
            else {
                var x = Convert.ToInt32(pageNumber);
                return dbSet.Include("AppUser").Where(r => r.RestaurantId == restaurantId && r.IsActive && r.isConfirmed).OrderBy(o => o.AddedDate).Skip(rowCount * x).Take(rowCount).ToList();
            }
        }

        public List<Review> GetIncludeRestaurant(Expression<Func<Review, bool>> predicate) {
            return dbSet.Include("Restaurant").Where(predicate).ToList();
        }

        public List<Review> GetIncludeUser(Expression<Func<Review, bool>> predicate) {
            return dbSet.Include("AppUser").Where(predicate).ToList();
        }

        public List<Review> GetConfirmedIncludeUserTakeX(int pageNumber, int shownAmount) {
            return dbSet.Include("AppUser").Where(r => r.IsActive && r.isConfirmed && r.Score != null).OrderBy(o => o.Id).Skip(pageNumber * shownAmount).Take(shownAmount).ToList();
        }

        public List<Review> GetUnconfirmedIncludeUserTakeX(int pageNumber, int shownAmount) {
            return dbSet.Include("AppUser").Where(r => r.IsActive && !r.isConfirmed && r.Score != null).OrderBy(o => o.Id).Skip(pageNumber * shownAmount).Take(shownAmount).ToList();
        }

        public List<Review> GetByUserIdIncludeRestaurantTakeX(string userId, int pageNumber, int shownAmount) {
            return dbSet.Include("Restaurant").Where(r => r.UserId == userId && r.IsActive && r.Score != null).OrderBy(o => o.Id).Skip(pageNumber * shownAmount).Take(shownAmount).ToList();
        }

        public List<Review> GetByRestaurantIdIncludeUserTakeX(int restaurantId, int pageNumber, int shownAmount) {
            return dbSet.Where(r => r.RestaurantId == restaurantId && r.IsActive && r.isConfirmed && r.Score != null).OrderBy(o => o.Id).Skip(Convert.ToInt32(pageNumber * shownAmount)).Take(shownAmount).ToList();
        }

        public object FilterByAll(Expression<Func<Review, bool>> expression) {
            IQueryable<Review> filtered = dbSet.Where(r => r.IsActive && r.isConfirmed && r.Restaurant.IsActive);
            if (expression != null) {
                filtered = filtered.Where(expression);
            }
            return filtered.GroupBy(r => new { Id = r.RestaurantId }).Select(g => new { Average = g.Average(p => p.Score), ID = g.Key.Id }).OrderByDescending(o => o.Average).ToList();
        }

        public object FilterByAllTakeX(int takeCount, Expression<Func<Review, bool>> expression) {
            IQueryable<Review> filtered = dbSet;
            filtered = filtered.Where(r => r.IsActive == true && r.isConfirmed == true);
            if (expression != null) {
                filtered = filtered.Where(expression);
            }
            return filtered.GroupBy(r => new { Id = r.RestaurantId }).Select(g => new { Average = g.Average(p => p.Score), ID = g.Key.Id }).OrderByDescending(o => o.Average).ToList();
        }

        public int GetCount() {
            return dbSet.Where(r => r.IsActive && r.Score != null).Count();
        }

        public int GetConfirmedCount() {
            return dbSet.Where(r => r.IsActive && r.isConfirmed && r.Score != null).Count();
        }

        public int GetUnconfirmedCount() {
            return dbSet.Where(r => r.IsActive && !r.isConfirmed && r.Score != null).Count();
        }

        public int GetCountByUserId(string userId) {
            return dbSet.Where(r => r.UserId == userId && r.IsActive && r.isConfirmed && r.Score != null).Count();
        }

        public int GetCountByRestaurantId(int id) {
            return dbSet.Where(r => r.RestaurantId == id && r.IsActive && r.isConfirmed && r.Score != null).Count();
        }

        public void DeleteByUserId(string userId) {
            var entitiesToBeDeleted = dbSet.Where(x => x.UserId == userId).ToList();
            foreach (var entity in entitiesToBeDeleted) {
                dbSet.Remove(entity);
            }
        }

        public string GetCountAllMonths() {
            var x = "";
            for (int i = 1; i <= 12; i++) {
                x += dbSet.Where(r => r.AddedDate.Month == i && r.AddedDate.Year == DateTime.Now.Year && r.Score != null && r.IsActive).Count() + ",";
            }
            return x;
        }

        public Review GetMostLikedRestaurantIdByUserId(string userId) {
            if (dbSet.Where(r => r.UserId == userId && r.IsActive && r.Score != null).Any()) {
                return dbSet.Where(r => r.UserId == userId && r.IsActive && r.Score != null)
                .OrderByDescending(o => o.Score)
                .First();
            }
            else {
                return null;
            }
        }

        public Review GetMostDislikedRestaurantIdByUserId(string userId) {
            if (dbSet.Where(r => r.UserId == userId && r.IsActive && r.Score != null).Any()) {
                return dbSet.Where(r => r.UserId == userId && r.IsActive && r.Score != null)
                    .OrderBy(o => o.Score).First();
            }
            else {
                return null;
            }
        }
    }
}
