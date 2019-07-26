using RestaurantReviews.DAL.Context;
using RestaurantReviews.Models.Contracts;
using RestaurantReviews.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviews.DAL.Repositories {
    class RestaurantFeatureRepository : BaseRepository<RestaurantFeature>, IRestaurantFeatureRepository {
        public RestaurantFeatureRepository(AppDbContext context) : base(context) {
        }

        public List<RestaurantFeature> GetByRestaurantIdIncludeFeatures(int id) {
            return dbSet.Include("Feature")
                .Where(f => f.RestaurantId == id)
                .ToList();
        }

        public void DeleteByRestaurantId(int restaurantId) {
            var entitiesToBeDeleted = dbSet.Where(x => x.RestaurantId == restaurantId)
                .ToList();

            foreach (var entity in entitiesToBeDeleted) {
                dbSet.Remove(entity);
            }
        }

        public void DeleteByFeatureId(int featureId) {
            var entitiesToBeDeleted = dbSet.Where(x => x.FeatureId == featureId)
                .ToList();

            foreach (var entity in entitiesToBeDeleted) {
                dbSet.Remove(entity);
            }
        }
    }
}
