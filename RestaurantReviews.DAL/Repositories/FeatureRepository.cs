using RestaurantReviews.DAL.Context;
using RestaurantReviews.Models.Contracts;
using RestaurantReviews.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviews.DAL.Repositories {
    public class FeatureRepository : BaseRepository<Feature>, IFeatureRepository {
        public FeatureRepository(AppDbContext context) : base(context) {
        }

        public List<Feature> GetActives() {
            return dbSet.Where(f => f.IsActive)
                .ToList();
        }

        public List<Feature> GetBySearchWord(string searchWord) {
            return dbSet.Where(f => f.Name.Contains(searchWord))
                .ToList();
        }
    }
}
