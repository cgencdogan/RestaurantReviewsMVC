using RestaurantReviews.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RestaurantReviews.Models.Contracts {
    public interface IRestaurantRepository : IBaseRepository<Restaurant> {
        List<Restaurant> GetAllIncludeDistricts(Expression<Func<Restaurant, bool>> predicate);
        List<Restaurant> GetAllIncludeDistricts();
        Restaurant GetByIdIncludeDistrict(int id);
        Restaurant GetByRestaurantKey(string restaurantKey);
        List<Restaurant> GetAllPassivesIncludeDistricts(Expression<Func<Restaurant, bool>> predicate);
        List<Restaurant> GetAllPassivesIncludeDistricts();
        Restaurant GetByIdIncludeDetails(int id);
        int GetCount();
        List<Restaurant> GetAllIncludeDistrictsFilterBySearchWordTakeX(string searchWord, int pageNumber, int shownAmount);
        int GetCountBySearchWord(string searchWord);
        List<Restaurant> GetAllIncludeDistrictsTakeX(int pageNumber, int shownAmount);
        List<Restaurant> GetAllPassivesIncludeDistrictsFilterBySearchWordTakeX(string searchWord, int pageNumber, int shownAmount);
        int GetPassiveCountBySearchWord(string searchWord);
        List<Restaurant> GetAllPassivesIncludeDistrictsTakeX(int pageNumber, int shownAmount);
        int GetPassiveCount();
        List<Restaurant> GetAllByRestaurantIdsIncludeDistricts(List<int> restaurantIds);
        List<Restaurant> GetBySearchWord(string searchWord);
        List<Restaurant> GetAllByRestaurantIds(List<int> restaurantIds);
        List<Restaurant> GetAllByRestaurantIdsTakeX(List<int> restaurantIds, int shownAmount);
    }
}
