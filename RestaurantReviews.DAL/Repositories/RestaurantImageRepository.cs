using RestaurantReviews.DAL.Context;
using RestaurantReviews.Models.Contracts;
using RestaurantReviews.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviews.DAL.Repositories {
    public class RestaurantImageRepository : BaseRepository<RestaurantImage>, IRestaurantImageRepository {
        public RestaurantImageRepository(AppDbContext context) : base(context) {
        }

        public List<RestaurantImage> GetByRestaurantId(int restaurantId) {
            return dbSet.Where(i => i.RestaurantId == restaurantId)
                .ToList();
        }

        public void DeleteByRestaurantId(int restaurantId) {
            var entitiesToBeDeleted = dbSet.Where(x => x.RestaurantId == restaurantId)
                .ToList();

            foreach (var entity in entitiesToBeDeleted) {
                dbSet.Remove(entity);
            }
        }

        public void DeleteByUserId(string userId) {
            var entitiesToBeDeleted = dbSet.Where(x => x.UploaderId == userId)
                .ToList();

            foreach (var entity in entitiesToBeDeleted) {
                dbSet.Remove(entity);
            }
        }

    }
}
