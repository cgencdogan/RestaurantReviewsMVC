using RestaurantReviews.Models.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviews.Models.Entities {
    public class RestaurantImage : BaseEntity {
        public string ImgFilePath { get; set; }
        public string ImgFilePathThumbnail { get; set; }

        [ForeignKey("AppUser")]
        public string UploaderId { get; set; }
        public virtual AppUser AppUser { get; set; }

        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
