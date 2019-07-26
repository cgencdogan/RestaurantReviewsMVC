using RestaurantReviews.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RestaurantReviews.Models.Contracts {
    public interface IRestaurantRepository : IBaseRepository<Restaurant> {
        int GetCount();
        int GetPassiveCount();
        int GetCountBySearchWord(string searchWord);
        int GetPassiveCountBySearchWord(string searchWord);
        Restaurant GetByRestaurantKey(string restaurantKey);
        Restaurant GetByIdIncludeDistrict(int id);
        Restaurant GetByIdIncludeDetails(int id);
        List<Restaurant> GetBySearchWord(string searchWord);
        List<Restaurant> GetByRestaurantIds(List<int> restaurantIds);
        List<Restaurant> GetByRestaurantIdsTakeX(List<int> restaurantIds, int shownAmount);
        List<Restaurant> GetIncludeDistricts();
        List<Restaurant> GetIncludeDistricts(Expression<Func<Restaurant, bool>> predicate);
        List<Restaurant> GetIncludeDistrictsTakeX(int pageNumber, int shownAmount);
        List<Restaurant> GetIncludeDistrictsFilterBySearchWordTakeX(string searchWord, int pageNumber, int shownAmount);
        List<Restaurant> GetByRestaurantIdsIncludeDistricts(List<int> restaurantIds);
        List<Restaurant> GetPassivesIncludeDistricts();
        List<Restaurant> GetPassivesIncludeDistricts(Expression<Func<Restaurant, bool>> predicate);
        List<Restaurant> GetPassivesIncludeDistrictsTakeX(int pageNumber, int shownAmount);
        List<Restaurant> GetPassivesIncludeDistrictsFilterBySearchWordTakeX(string searchWord, int pageNumber, int shownAmount);
    }
}
