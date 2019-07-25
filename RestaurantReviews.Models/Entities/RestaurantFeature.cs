using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviews.Models.Entities {
    public class RestaurantFeature {
        [Key, ForeignKey("Restaurant"), Column(Order = 10)]
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        [Key, ForeignKey("Feature"), Column(Order = 20)]
        public int FeatureId { get; set; }
        public virtual Feature Feature { get; set; }
    }
}
