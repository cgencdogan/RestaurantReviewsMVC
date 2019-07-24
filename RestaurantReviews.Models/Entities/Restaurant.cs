using RestaurantReviews.Models.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviews.Models.Entities {
    public class Restaurant : BaseEntity {

        public string RestaurantKey { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Adress { get; set; }

        [MaxLength(10)]
        public string PhoneNumber { get; set; }

        public string CoverImagePath { get; set; }

        [ForeignKey("District")]
        public int DistrictId { get; set; }
        public virtual District District { get; set; }

        [ForeignKey("AppUser")]
        public string AddedBy { get; set; }
        public virtual AppUser AppUser { get; set; }

        //public virtual ICollection<Category> Categories { get; set; }
    }
}

