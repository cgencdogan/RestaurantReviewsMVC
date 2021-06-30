namespace RestaurantReviews.DAL.Migrations
{
    using RestaurantReviews.Models.Entities;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<RestaurantReviews.DAL.Context.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(RestaurantReviews.DAL.Context.AppDbContext context)
        {
        }
    }
}
