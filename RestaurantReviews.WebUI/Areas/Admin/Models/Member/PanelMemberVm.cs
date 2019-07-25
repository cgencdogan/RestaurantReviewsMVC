namespace RestaurantReviews.WebUI.Areas.Admin.Models {
    public class PanelMemberVm {
        public string AppUserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int ReviewCount { get; set; }
    }
}