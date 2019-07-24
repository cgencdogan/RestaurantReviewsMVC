using System.Collections.Generic;

namespace RestaurantReviews.WebUI.Models {
    public class ReviewListVm {
        public List<ReviewVm> Reviews { get; set; }
        public int RestaurantId { get; set; }
        public int PageNumber { get; set; }
        public int ReviewCount { get; set; }
        public double MaxPage { get; set; }
    }
}