using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

[assembly: OwinStartup(typeof(RestaurantReviews.WebUI.App_Start.Startup))]

namespace RestaurantReviews.WebUI.App_Start {
    public class Startup {
        public void Configuration(IAppBuilder app) {
            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/account/login"),
                ExpireTimeSpan = TimeSpan.FromMinutes(2880),
                SlidingExpiration = true
            });
        }
    }
}