using RestaurantReviews.Models.Entities;
using System.Collections.Generic;

namespace RestaurantReviews.Models.Contracts {
    public interface ICategoryRepository : IBaseRepository<Category> {
        int GetIdBySearchWord(string searchWord);
        List<Category> GetByCategoryIds(List<int> categoryIds);
    }
}
