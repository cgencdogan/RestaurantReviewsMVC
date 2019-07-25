using System.Web.Mvc;
using System.Web.Routing;

namespace RestaurantReviews.WebUI {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "RestaurantDetail",
                url: "restoran-detay",
                defaults: new { controller = "Restaurant", action = "Detail", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "HomeIndex",
                url: "tum-restoranlar",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
