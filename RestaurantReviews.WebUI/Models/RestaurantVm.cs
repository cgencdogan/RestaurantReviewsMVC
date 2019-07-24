using RestaurantReviews.Models.Entities;
using System.Collections.Generic;

namespace RestaurantReviews.WebUI.Models {
    public class RestaurantVm {
        public List<RestaurantListVm> RestaurantListVm { get; set; }
        public Category Category { get; set; }
        public District District { get; set; }
        public List<Category> Categories { get; set; }
        public List<District> Districts { get; set; }
        public string SearchWord { get; set; }
        public double MaxPage { get; set; }

    }

}