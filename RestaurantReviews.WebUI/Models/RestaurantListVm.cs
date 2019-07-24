using RestaurantReviews.Models.Entities;

namespace RestaurantReviews.WebUI.Models {
    public class RestaurantListVm {
        public int Id { get; set; }
        public string Name { get; set; }
        public District District { get; set; }
        public decimal? Score { get; set; }
        public int ReviewCount { get; set; }
        public string PicturePath { get; set; }
    }
}