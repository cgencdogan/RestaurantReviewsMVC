using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.WebUI.Models {
    public class SignUpVm {
        [Required]
        public string Email { get; set; }
        [Required]
        public string SignupUsername { get; set; }
        [Required]
        public string SignupPassword { get; set; }
    }
}