using System.Web.Optimization;

namespace RestaurantReviews.WebUI.App_Start {
    public class BundleConfig {
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/panelendjs").Include(
                        "~/Scripts/AdminTemplate/bootstrap.bundle.min.js",
                        "~/Scripts/AdminTemplate/jquery.easing.min.js",
                        "~/Scripts/AdminTemplate/sb-admin-2.js"));

            bundles.Add(new ScriptBundle("~/bundles/panelheadjs").Include(
                        "~/Scripts/jquery-3.4.1.min.js",
                        "~/Content/Assets/sweetalert2.all.js"));

            bundles.Add(new StyleBundle("~/bundles/panelcss").Include(
                        "~/Content/Custom/PanelArea/sb-admin-2.css",
                        "~/Content/Assets/Panel/sweetalert2-panel.css",
                        "~/Content/Custom/PanelLayoutStyle.css").Include("~/Content/Custom/PanelArea/all.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/layoutcss").Include(
                        "~/Content/Assets/bootstrap.css", new CssRewriteUrlTransform()).Include(
                        "~/Content/toastr.min.css",
                        "~/Content/Assets/sweetalert2.css",
                        "~/Content/Custom/LayoutStyle.css"));

            bundles.Add(new ScriptBundle("~/bundles/layoutjs").Include(
                        "~/Scripts/jquery-3.4.1.min.js",
                        "~/Scripts/bootstrap.js",
                        "~/Content/Assets/sweetalert2.all.js",
                        "~/Scripts/toastr.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/layoutendjs").Include(
                        "~/Scripts/Custom/LoginRegister.js"));
        }
    }
}