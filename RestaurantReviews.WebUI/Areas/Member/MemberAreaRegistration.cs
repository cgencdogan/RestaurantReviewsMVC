using System.Web.Mvc;

namespace RestaurantReviews.WebUI.Areas.Member
{
    public class MemberAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Member";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MemberMemberDetails",
                "profil",
                new { controller = "Member", action = "Details", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "MemberMemberChangeMail",
                "kullanici-islemleri/eposta-degistir",
                new { controller = "Member", action = "ChangeMail", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "MemberMemberChangePassword",
                "kullanici-islemleri/sifre-degistir",
                new { controller = "Member", action = "ChangePassword", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "MemberMemberChangeAvatar",
                "kullanici-islemleri/profil-resmi-degistir",
                new { controller = "Member", action = "ChangeAvatar", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "MemberReviewList",
                "kullanici-islemleri/yorumlar",
                new { controller = "ReviewMember", action = "List", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "MemberMemberPublicProfile",
                "kullanici",
                new { controller = "Member", action = "PublicProfile", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Member_default",
                "Member/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}