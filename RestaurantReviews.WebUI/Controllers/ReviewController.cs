using Microsoft.AspNet.Identity;
using RestaurantReviews.BLL.Service;
using RestaurantReviews.Models.Entities;
using System.Web.Mvc;

namespace RestaurantReviews.WebUI.Controllers {
    public class ReviewController : Controller {
        DataService service;
        public ReviewController() {
            service = new DataService();
        }

        [HttpPost]
        public ActionResult Add(Review data) {
            var review = new Review();
            review.RestaurantId = data.RestaurantId;
            review.Content = data.Content;
            review.UserId = User.Identity.GetUserId();
            review.Score = data.Score;
            review.isConfirmed = false;

            service.Uow.Reviews.Insert(review);
            service.Uow.Save();

            return Redirect("/");
        }
    }
}