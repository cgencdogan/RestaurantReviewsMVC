using RestaurantReviews.BLL.Service;
using RestaurantReviews.WebUI.Areas.Admin.Models;
using System.Linq;
using System.Web.Mvc;

namespace RestaurantReviews.WebUI.Areas.Admin.Controllers {
    [Authorize(Roles = "admin")]
    public class AdminController : Controller {
        DataService service;
        public AdminController() {
            service = new DataService();
        }

        public ActionResult Dashboard() {
            var model = new DashboardVm();
            model.RestaurantCount = service.Uow.Restaurants.GetCount();
            model.ReviewCount = service.Uow.Reviews.GetCount();
            model.UserCount = service.Uow.Users.Users.Where(u => u.EmailConfirmed == true && u.IsActive).Count();
            return View(model);
        }
    }
}