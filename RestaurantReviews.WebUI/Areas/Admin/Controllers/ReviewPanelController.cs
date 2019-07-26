using RestaurantReviews.BLL.Service;
using RestaurantReviews.Models.Entities;
using RestaurantReviews.WebUI.Areas.Admin.Models;
using RestaurantReviews.WebUI.Utils;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RestaurantReviews.WebUI.Areas.Admin.Controllers {
    [Authorize(Roles = "admin, moderator")]
    public class ReviewPanelController : Controller {
        DataService service;
        public ReviewPanelController() {
            service = new DataService();
        }

        public ActionResult List(bool confirmed = true, int pageNumber = 0) {
            int reviewCount;
            var reviews = new List<Review>();
            var model = new PanelReviewListVm();
            if (confirmed == true) {
                reviewCount = service.Uow.Reviews.GetConfirmedCount();
                reviews = service.Uow.Reviews.GetConfirmedIncludeUserTakeX(pageNumber, PageUtil.PanelReviewShownCount);
                model.Confirmed = true;
            }
            else {
                reviewCount = service.Uow.Reviews.GetUnconfirmedCount();
                reviews = service.Uow.Reviews.GetUnconfirmedIncludeUserTakeX(pageNumber, PageUtil.PanelReviewShownCount);
                model.Confirmed = false;
            }
            model.PanelReviews = new List<PanelReviewVm>();
            foreach (var review in reviews) {
                model.PanelReviews.Add(new PanelReviewVm() {
                    ReviewId = review.Id,
                    RestaurantName = review.Restaurant.Name,
                    RestaurantId = review.RestaurantId,
                    Username = review.AppUser.UserName,
                    Score = review.Score,
                    ReviewDate = review.AddedDate.ToString(),
                    ReviewContent = review.Content
                });
            }
            var maxPage = Math.Ceiling(reviewCount / Convert.ToDouble(PageUtil.PanelReviewShownCount));
            model.PageNumber = pageNumber;
            model.ShownCount = PageUtil.PanelReviewShownCount;
            model.MaxPage = maxPage;
            return View(model);
        }

        [HttpPost]
        public ActionResult Confirm(int id) {
            var review = service.Uow.Reviews.GetById(id);
            review.isConfirmed = true;
            service.Uow.Reviews.Update(review);
            service.Uow.Save();
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Update(ReviewUpdateVm data) {
            var review = service.Uow.Reviews.GetById(data.Id);
            review.Content = data.Content;
            service.Uow.Reviews.Update(review);
            service.Uow.Save();
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Delete(int id) {
            var review = service.Uow.Reviews.GetById(id);
            review.IsActive = false;
            service.Uow.Reviews.Update(review);
            service.Uow.Save();
            return RedirectToAction("List");
        }
    }
}