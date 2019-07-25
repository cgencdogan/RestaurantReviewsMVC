using RestaurantReviews.Models.Entities;
using System.Collections.Generic;

namespace RestaurantReviews.WebUI.Models {
    public class ImageListVm {
        public List<RestaurantImage> RestaurantImages { get; set; }
        public int RestaurantId { get; set; }
        public int PageNumber { get; set; }
    }
}