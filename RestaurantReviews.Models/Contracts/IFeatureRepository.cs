using RestaurantReviews.Models.Entities;
using System.Collections.Generic;

namespace RestaurantReviews.Models.Contracts {
    public interface IFeatureRepository : IBaseRepository<Feature> {
        List<Feature> GetActives();
        List<Feature> GetBySearchWord(string searchWord);
    }
}
