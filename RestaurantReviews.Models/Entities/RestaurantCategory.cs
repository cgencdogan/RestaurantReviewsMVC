using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviews.Models.Entities {
    public class RestaurantCategory {

        [Key, ForeignKey("Restaurant"), Column(Order = 10)]
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        [Key, ForeignKey("Category"), Column(Order = 20)]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
