using RestaurantReviews.Models.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviews.Models.Entities {
    public class Review : BaseEntity {
        [MaxLength(2500)]
        public string Content { get; set; }
        public byte? Score { get; set; }

        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        public bool isConfirmed { get; set; }
    }
}
