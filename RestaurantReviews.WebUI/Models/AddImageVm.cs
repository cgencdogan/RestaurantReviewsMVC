using System.Web;

namespace RestaurantReviews.WebUI.Models {
    public class AddImageVm {
        public HttpPostedFileBase RestaurantImage { get; set; }
        public int RestaurantId { get; set; }
    }
}