namespace RestaurantReviews.WebUI.Areas.Member.Models {
    public class ChangePasswordVm {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirm { get; set; }
    }
}