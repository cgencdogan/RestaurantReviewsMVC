using RestaurantReviews.Models.Entities;
using System.Collections.Generic;

namespace RestaurantReviews.WebUI.Models {
    public class SugestionListVm {
        public List<RestaurantListVm> RestaurantsInSameDistrict { get; set; }
        public List<List<RestaurantListVm>> RestaurantsInSameCategories { get; set; }
        public District District { get; set; }
        public List<Category> Categories { get; set; }
    }
}