using RestaurantReviews.DAL.Context;
using RestaurantReviews.Models.Contracts;
using RestaurantReviews.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviews.DAL.Repositories {
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository {
        public CategoryRepository(AppDbContext context) : base(context) {
        }
        public int GetIdBySearchWord(string searchWord) {
            if (dbSet.Any(c => c.Name.Contains(searchWord))) {
                return dbSet.Where(c => c.Name.Contains(searchWord))
                    .FirstOrDefault()
                    .Id;
            }
            else {
                return 0;
            }
        }

        public List<Category> GetByCategoryIds(List<int> categoryIds) {
            return dbSet.Where(c => categoryIds.Contains(c.Id) && c.IsActive)
                .ToList();
        }
    }
}
