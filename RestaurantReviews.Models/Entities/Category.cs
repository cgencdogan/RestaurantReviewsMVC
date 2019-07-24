using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.Models.Entities {
    public class Category : BaseEntity {
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
