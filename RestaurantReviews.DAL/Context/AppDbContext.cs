using Microsoft.AspNet.Identity.EntityFramework;
using RestaurantReviews.Models.Entities;
using System.Data.Entity;

namespace RestaurantReviews.DAL.Context {
    public class AppDbContext : IdentityDbContext {
        public AppDbContext() {
            Database.Connection.ConnectionString = @"server=DESKTOP-J2137VK;database=RestaurantReviewsDb;Integrated Security=true";
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<RestaurantCategory> RestaurantCategories { get; set; }
        public DbSet<RestaurantFeature> RestaurantFeatures { get; set; }
        public DbSet<RestaurantImage> RestaurantImages { get; set; }
    }
}
