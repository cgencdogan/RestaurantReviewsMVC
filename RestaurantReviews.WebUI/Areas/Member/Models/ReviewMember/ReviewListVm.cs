using RestaurantReviews.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReviews.WebUI.Areas.Member.Models {
    public class ReviewListVm {
        public List<Review> MyReviews { get; set; }
        public int PageNumber { get; set; }
        public int ShownCount { get; set; }
        public double MaxPage { get; set; }
    }
}