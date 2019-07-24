using RestaurantReviews.Models.Entities;
using System.Collections.Generic;

namespace RestaurantReviews.WebUI.Areas.Admin.Models {
    public class FeatureVm {
        public List<Feature> Features { get; set; }
        public string SearchWord { get; set; }
        public int PageNumber { get; set; }
        public int ShownCount { get; set; }
        public double MaxPage { get; set; }
    }
}