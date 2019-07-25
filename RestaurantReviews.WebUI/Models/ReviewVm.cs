using System;

namespace RestaurantReviews.WebUI.Models {
    public class ReviewVm {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte? Score { get; set; }
        public int UserReviewCount { get; set; }
        public string Content { get; set; }
        public DateTime ReviewDate { get; set; }
        public string ProfilePicPath { get; set; }
    }
}