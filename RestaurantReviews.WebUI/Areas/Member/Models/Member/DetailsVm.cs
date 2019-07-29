using RestaurantReviews.Models.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReviews.WebUI.Areas.Member.Models {
    public class DetailsVm {
        public AppUser AppUser { get; set; }
        public int ReviewCount { get; set; }
    }
}