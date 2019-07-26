using RestaurantReviews.DAL.Context;
using RestaurantReviews.Models.Contracts;
using RestaurantReviews.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RestaurantReviews.DAL.Repositories {
    public class RestaurantCategoryRepository : BaseRepository<RestaurantCategory>, IRestaurantCategoryRepository {
        public RestaurantCategoryRepository(AppDbContext context) : base(context) {
        }

        public List<RestaurantCategory> GetIncludeCategories(Expression<Func<RestaurantCategory, bool>> predicate) {
            return dbSet.Include("Category")
                .Where(predicate)
                .ToList();
        }
        public List<RestaurantCategory> GetByCategoryId(int categoryId) {
            return dbSet.Where(c => c.CategoryId == categoryId)
                .ToList();
        }

        public List<RestaurantCategory> GetByRestaurantIdIncludeCategories(int restaurantId) {
            return dbSet.Include("Category")
                .Where(c => c.RestaurantId == restaurantId)
                .ToList();
        }

        public List<int> GetByRestaurantId(int restaurantId) {
            var ids = new List<int>();

            var categoryIds = dbSet.Where(c => c.RestaurantId == restaurantId)
                .Select(x => new { x.CategoryId })
                .ToList();

            foreach (var item in categoryIds) {
                ids.Add(item.CategoryId);
            }
            return ids;
        }

        public void DeleteByRestaurantId(int restaurantId) {
            var entitiesToBeDeleted = dbSet.Where(x => x.RestaurantId == restaurantId)
                .ToList();

            foreach (var entity in entitiesToBeDeleted) {
                dbSet.Remove(entity);
            }
        }

        public void DeleteByCategoryId(int categoryId) {
            var entitiesToBeDeleted = dbSet.Where(x => x.CategoryId == categoryId)
                .ToList();

            foreach (var entity in entitiesToBeDeleted) {
                dbSet.Remove(entity);
            }
        }

        public int GetCountByCategoryId(int categoryId) {
            return dbSet.Where(c => c.CategoryId == categoryId)
                .Count();
        }
    }
}
