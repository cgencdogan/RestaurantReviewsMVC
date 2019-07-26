using RestaurantReviews.DAL.Context;
using RestaurantReviews.Models.Contracts;
using RestaurantReviews.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RestaurantReviews.DAL.Repositories {
    public class RestaurantRepository : BaseRepository<Restaurant>, IRestaurantRepository {
        public RestaurantRepository(AppDbContext context) : base(context) {
        }

        public int GetCount() {
            return dbSet.Where(r => r.IsActive)
                .Count();
        }

        public int GetPassiveCount() {
            return dbSet.Where(r => !r.IsActive)
                .Count();
        }

        public int GetCountBySearchWord(string searchWord) {
            return dbSet.Where(r => r.IsActive && r.Name.Contains(searchWord))
                .Count();
        }

        public int GetPassiveCountBySearchWord(string searchWord) {
            return dbSet.Where(r => !r.IsActive && r.Name.Contains(searchWord))
                .Count();
        }

        public Restaurant GetByRestaurantKey(string restaurantKey) {
            return dbSet.FirstOrDefault(r => r.RestaurantKey == restaurantKey);
        }

        public Restaurant GetByIdIncludeDistrict(int id) {
            return dbSet.Include("District")
                .FirstOrDefault(x => x.Id == id);
        }

        public Restaurant GetByIdIncludeDetails(int id) {
            return dbSet.Include("District")
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Restaurant> GetBySearchWord(string searchWord) {
            return dbSet.Where(r => r.Name.Contains(searchWord) && r.IsActive)
                .ToList();
        }

        public List<Restaurant> GetByRestaurantIds(List<int> restaurantIds) {
            return dbSet.Where(r => restaurantIds.Contains(r.Id) && r.IsActive)
                .ToList();
        }

        public List<Restaurant> GetByRestaurantIdsTakeX(List<int> restaurantIds, int shownAmount) {
            return dbSet.Where(r => restaurantIds.Contains(r.Id) && r.IsActive)
                .OrderBy(o => o.Id)
                .Take(shownAmount)
                .ToList();
        }

        public List<Restaurant> GetIncludeDistricts() {
            return dbSet.Include("District")
                .Where(r => r.IsActive == true)
                .ToList();
        }

        public List<Restaurant> GetIncludeDistricts(Expression<Func<Restaurant, bool>> predicate) {
            return dbSet.Include("District")
                .Where(r => r.IsActive == true)
                .Where(predicate)
                .ToList();
        }

        public List<Restaurant> GetIncludeDistrictsTakeX(int pageNumber, int shownAmount) {
            return dbSet.Include("District")
                .Where(r => r.IsActive)
                .OrderBy(o => o.Id)
                .Skip(pageNumber * shownAmount)
                .Take(shownAmount)
                .ToList();
        }

        public List<Restaurant> GetIncludeDistrictsFilterBySearchWordTakeX(string searchWord, int pageNumber, int shownAmount) {
            return dbSet.Include("District")
                .Where(r => r.IsActive && r.Name.Contains(searchWord))
                .OrderBy(o => o.Id)
                .Skip(pageNumber * shownAmount)
                .Take(shownAmount)
                .ToList();
        }

        public List<Restaurant> GetByRestaurantIdsIncludeDistricts(List<int> restaurantIds) {
            return dbSet.Include("District")
                .Where(r => restaurantIds.Contains(r.Id) && r.IsActive)
                .ToList();
        }

        public List<Restaurant> GetPassivesIncludeDistricts() {
            return dbSet.Include("District")
                .Where(r => !r.IsActive)
                .ToList();
        }

        public List<Restaurant> GetPassivesIncludeDistricts(Expression<Func<Restaurant, bool>> predicate) {
            return dbSet.Include("District")
                .Where(r => !r.IsActive)
                .Where(predicate)
                .ToList();
        }

        public List<Restaurant> GetPassivesIncludeDistrictsTakeX(int pageNumber, int shownAmount) {
            return dbSet.Include("District")
                .Where(r => !r.IsActive)
                .OrderBy(o => o.Id)
                .Skip(pageNumber * shownAmount)
                .Take(shownAmount)
                .ToList(); ;
        }

        public List<Restaurant> GetPassivesIncludeDistrictsFilterBySearchWordTakeX(string searchWord, int pageNumber, int shownAmount) {
            return dbSet.Include("District")
                .Where(r => !r.IsActive && r.Name.Contains(searchWord))
                .OrderBy(o => o.Id)
                .Skip(pageNumber * shownAmount)
                .Take(shownAmount)
                .ToList();
        }
    }
}
