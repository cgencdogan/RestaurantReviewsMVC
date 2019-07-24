using RestaurantReviews.Models.Entities;
using System.Collections.Generic;

namespace RestaurantReviews.WebUI.Models {
    public class RestaurantListHome {
        public List<Review> Reviews { get; set; }
        public Restaurant Restaurant { get; set; }
        public District District { get; set; }
        public double Score { get; set; }

    }
}