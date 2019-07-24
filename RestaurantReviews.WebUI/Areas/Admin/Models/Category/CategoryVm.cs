using RestaurantReviews.Models.Entities;

namespace RestaurantReviews.WebUI.Areas.Admin.Models {
    public class CategoryVm {
        public Category Category { get; set; }
        public int Count { get; set; }
    }
}