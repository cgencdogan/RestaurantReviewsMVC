using System.Web.Mvc;

namespace RestaurantReviews.WebUI.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdminAdminDashboard",
                "yonetim-paneli/genel-gorunum",
                new {controller = "Admin" ,action = "Dashboard", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "AdminMemberList",
                "yonetim-paneli/kullanici-listesi",
                new { controller = "Member", action = "List", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "AdminMemberPassiveList",
                "yonetim-paneli/pasif-kullanici-listesi",
                new { controller = "Member", action = "PassiveList", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "AdminRestaurantAdd",
                "yonetim-paneli/restoran-ekle",
                new { controller = "RestaurantPanel", action = "Add", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "AdminRestaurantList",
                "yonetim-paneli/restoran-listesi",
                new { controller = "RestaurantPanel", action = "List", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "AdminRestaurantPassiveList",
                "yonetim-paneli/pasif-restoran-listesi",
                new { controller = "RestaurantPanel", action = "PassiveList", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "AdminRestaurantUpdate",
                "yonetim-paneli/restoran-guncelle",
                new { controller = "RestaurantPanel", action = "Update", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "AdminReviewList",
                "yonetim-paneli/yorum-listesi",
                new { controller = "ReviewPanel", action = "List", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "AdminCategoryList",
                "yonetim-paneli/kategori-listesi",
                new { controller = "CategoryPanel", action = "List", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "AdminFeatureList",
                "yonetim-paneli/servis-listesi",
                new { controller = "FeaturePanel", action = "List", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}