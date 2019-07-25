using RestaurantReviews.Models.Entities;
using System.Collections.Generic;
using System.Web;

namespace RestaurantReviews.WebUI.Areas.Admin.Models {
    public class AddRestaurantVm {
        public List<District> Districts { get; set; }
        public List<Feature> Features { get; set; }
        public List<Category> Categories { get; set; }
        public string RestaurantName { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public int DistrictId { get; set; }
        public string AddedBy { get; set; }
        public List<int> FeatureIds { get; set; }
        public List<int> CategoryIds { get; set; }
        public HttpPostedFileBase RestaurantImage { get; set; }
    }
}