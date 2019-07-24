using System.Collections.Generic;

namespace RestaurantReviews.WebUI.Areas.Admin.Models {
    public class PanelReviewListVm {
        public List<PanelReviewVm> PanelReviews { get; set; }
        public bool Confirmed { get; set; }
        public int PageNumber { get; set; }
        public int ShownCount { get; set; }
        public double MaxPage { get; set; }
    }
}