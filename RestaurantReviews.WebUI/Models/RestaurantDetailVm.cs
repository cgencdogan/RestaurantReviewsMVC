using RestaurantReviews.Models.Entities;
using System.Collections.Generic;

namespace RestaurantReviews.WebUI.Models {
    public class RestaurantDetailVm {
        public Restaurant Restaurant { get; set; }
        public List<string> RestaurantFeatures { get; set; }
        public List<Category> RestaurantCategories { get; set; }
        public decimal? Score { get; set; }
        public int ReviewCount { get; set; }
        public int PageNumber { get; set; }
        public string CoverPicPath { get; set; }
    }
}