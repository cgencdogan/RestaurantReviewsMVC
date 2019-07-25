using RestaurantReviews.Models.Entities;

namespace RestaurantReviews.WebUI.Areas.Admin.Models {
    public class PanelRestaurantVm {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public District District { get; set; }
    }
}