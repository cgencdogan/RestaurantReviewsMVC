using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RestaurantReviews.Models.Entities.Identity;

namespace RestaurantReviews.Models.Contracts {
    public interface IMembershipRepository {
        UserStore<AppUser> UserStore { get; }
        UserManager<AppUser> Users { get; }
        RoleManager<AppRole> Roles { get; }
    }
}
