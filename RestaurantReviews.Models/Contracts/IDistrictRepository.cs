using RestaurantReviews.Models.Entities;

namespace RestaurantReviews.Models.Contracts {
    public interface IDistrictRepository : IBaseRepository<District> {
        int GetIdBySearchWord(string searchWord);
    }
}
