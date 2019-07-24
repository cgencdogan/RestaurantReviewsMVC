using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviews.Models.Entities.Identity {
    public class AppUser : IdentityUser {

        public string ProfilePicPath { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ProfilePicUploadDate { get; set; }

        public string EmailVerificationCode { get; set; }

        public bool IsActive { get; set; }
    }
}
