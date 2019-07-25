using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RestaurantReviews.Models.Entities.Identity;
using System;

namespace RestaurantReviews.Models.Contracts {
    public interface IUnitOfWork : IDisposable {
        ICategoryRepository Categories { get; }
        IRestaurantRepository Restaurants { get; }
        IReviewRepository Reviews { get; }
        IRestaurantImageRepository RestaurantImages { get; }
        IRestaurantCategoryRepository RestaurantCategories { get; }
        IDistrictRepository Districts { get; }
        IMembershipRepository Members { get; }
        IFeatureRepository Features { get; }
        IRestaurantFeatureRepository RestaurantFeatures { get; }
        UserStore<AppUser> UserStore { get; }
        UserManager<AppUser> Users { get; }
        RoleManager<AppRole> Roles { get; }
        int Save();
    }
}
