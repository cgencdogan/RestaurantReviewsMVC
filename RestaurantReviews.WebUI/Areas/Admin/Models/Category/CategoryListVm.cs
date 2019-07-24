using System.Collections.Generic;

namespace RestaurantReviews.WebUI.Areas.Admin.Models {
    public class CategoryListVm {
        public List<CategoryVm> Categories { get; set; }
        public string SearchWord { get; set; }
        public int PageNumber { get; set; }
        public double MaxPage { get; set; }
        public int ShownAmount { get; set; }
    }
}