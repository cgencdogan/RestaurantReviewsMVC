using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RestaurantReviews.DAL.Context;
using RestaurantReviews.Models.Contracts;
using RestaurantReviews.Models.Entities.Identity;
using System;

namespace RestaurantReviews.DAL.Repositories.UnitOfWork {
    public class UnitOfWork : IUnitOfWork {

        private AppDbContext context;
        private ICategoryRepository _categories;
        private IRestaurantRepository _restaurants;
        private IReviewRepository _reviews;
        private IRestaurantImageRepository _restaurantImages;
        private IRestaurantCategoryRepository _restaurantCategories;
        private IDistrictRepository _districts;
        private IMembershipRepository _members;
        private IFeatureRepository _features;
        private IRestaurantFeatureRepository _restaurantFeatures;
        private UserStore<AppUser> _userStore;
        private UserManager<AppUser> _userManager;
        private RoleStore<AppRole> _roleStore;
        private RoleManager<AppRole> _roleManager;

        public UnitOfWork(AppDbContext context) {
            this.context = context;
            _userStore = new UserStore<AppUser>(this.context);
            _roleStore = new RoleStore<AppRole>(this.context);
        }

        public ICategoryRepository Categories {
            get {
                if (_categories == null)
                    _categories = new CategoryRepository(context);

                return _categories;
            }
        }

        public IDistrictRepository Districts {
            get {
                if (_districts == null)
                    _districts = new DistrictRepository(context);

                return _districts;
            }
        }

        public IRestaurantRepository Restaurants {
            get {
                if (_restaurants == null)
                    _restaurants = new RestaurantRepository(context);

                return _restaurants;
            }
        }

        public IReviewRepository Reviews {
            get {
                if (_reviews == null)
                    _reviews = new ReviewRepository(context);

                return _reviews;
            }
        }

        public IRestaurantImageRepository RestaurantImages {
            get {
                if (_restaurantImages == null)
                    _restaurantImages = new RestaurantImageRepository(context);

                return _restaurantImages;
            }
        }

        public IRestaurantCategoryRepository RestaurantCategories {
            get {
                if (_restaurantCategories == null)
                    _restaurantCategories = new RestaurantCategoryRepository(context);

                return _restaurantCategories;
            }
        }

        public IMembershipRepository Members {
            get {
                if (_members == null)
                    _members = new MembershipRepository(context);

                return _members;
            }
        }

        public IFeatureRepository Features {
            get {
                if (_features == null)
                    _features = new FeatureRepository(context);

                return _features;
            }
        }

        public IRestaurantFeatureRepository RestaurantFeatures {
            get {
                if (_restaurantFeatures == null)
                    _restaurantFeatures = new RestaurantFeatureRepository(context);

                return _restaurantFeatures;
            }
        }

        public UserStore<AppUser> UserStore {
            get {
                return _userStore;
            }
        }

        public UserManager<AppUser> Users {
            get {
                if (_userManager == null)
                    _userManager = new UserManager<AppUser>(_userStore);

                return _userManager;
            }
        }

        public RoleManager<AppRole> Roles {
            get {
                if (_roleManager == null)
                    _roleManager = new RoleManager<AppRole>(_roleStore);

                return _roleManager;
            }
        }


        public int Save() {
            return context.SaveChanges();
        }

        private bool disposed = false;
        private void Dispose(bool disposing) {
            if (!disposed)
                if (disposing)
                    context.Dispose();
            disposed = true;
        }
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
