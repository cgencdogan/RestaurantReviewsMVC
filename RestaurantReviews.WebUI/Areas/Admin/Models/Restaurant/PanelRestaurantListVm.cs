using System.Collections.Generic;

namespace RestaurantReviews.WebUI.Areas.Admin.Models {
    public class PanelRestaurantListVm {
        public List<PanelRestaurantVm> RestaurantList { get; set; }
        public string SearchWord { get; set; }
        public int PageNumber { get; set; }
        public int ShownCount { get; set; }
        public double MaxPage { get; set; }
    }
}