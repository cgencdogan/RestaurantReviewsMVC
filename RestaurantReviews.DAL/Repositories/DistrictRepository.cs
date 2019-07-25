using RestaurantReviews.DAL.Context;
using RestaurantReviews.Models.Contracts;
using RestaurantReviews.Models.Entities;
using System.Linq;

namespace RestaurantReviews.DAL.Repositories {
    public class DistrictRepository : BaseRepository<District>, IDistrictRepository {
        public DistrictRepository(AppDbContext context) : base(context) {
        }
        public int GetIdBySearchWord(string searchWord) {
            if (dbSet.Any(d => d.Name.Contains(searchWord))) {
                return dbSet.Where(d => d.Name.Contains(searchWord)).FirstOrDefault().Id;
            }
            else {
                return 0;
            }
        }
    }
}
