namespace RestaurantReviews.WebUI.Areas.Admin.Models {
    public class PanelReviewVm {
        public int ReviewId { get; set; }
        public string Username { get; set; }
        public string RestaurantName { get; set; }
        public int RestaurantId { get; set; }
        public string ReviewDate { get; set; }
        public byte? Score { get; set; }
        public string ReviewContent { get; set; }
    }
}