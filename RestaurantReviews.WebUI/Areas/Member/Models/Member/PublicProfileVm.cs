using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReviews.WebUI.Areas.Member.Models {
    public class PublicProfileVm {
        public string AppUserId { get; set; }
        public string Username { get; set; }
        public string ProfilePicPath { get; set; }
        public int ReviewCount { get; set; }
        public string LikedRestaurantName { get; set; }
        public int LikedRestaurantId { get; set; }
        public byte? LikedRestaurantScore { get; set; }
        public string DislikedRestaurantName { get; set; }
        public int DislikedRestaurantId { get; set; }
        public byte? DislikedRestaurantScore { get; set; }
    }
}