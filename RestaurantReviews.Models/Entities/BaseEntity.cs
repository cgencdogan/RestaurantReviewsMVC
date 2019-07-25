using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviews.Models.Entities {
    public class BaseEntity {

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime AddedDate { get; set; } = DateTime.Now;

        [Column(TypeName = "datetime2")]
        public DateTime LastUpdatedDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;
    }
}
