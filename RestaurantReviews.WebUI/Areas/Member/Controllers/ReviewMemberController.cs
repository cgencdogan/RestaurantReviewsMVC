using Microsoft.AspNet.Identity;
using RestaurantReviews.BLL.Service;
using RestaurantReviews.Models.Entities;
using RestaurantReviews.WebUI.Areas.Member.Models;
using RestaurantReviews.WebUI.Utils;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RestaurantReviews.WebUI.Areas.Member.Controllers {
    [Authorize(Roles = "admin,moderator,default_user")]
    public class ReviewMemberController : Controller {
        DataService service;
        public ReviewMemberController() {
            service = new DataService();
        }

        public ActionResult List(int pageNumber = 0) {
            var userId = User.Identity.GetUserId();
            var model = new ReviewListVm();
            model.MyReviews = service.Uow.Reviews.GetByUserIdIncludeRestaurantTakeX(userId, pageNumber, PageUtil.PanelMyReviewShownCount);
            //if (pageNumber == 0) {
            //    model = service.Uow.Reviews.GetIncludeRestaurant(r => r.UserId == userId && r.IsActive && r.Score != null).Take(PageUtil.PanelMyReviewShownCount).ToList();
            //}
            //else {
            //    model = service.Uow.Reviews.GetIncludeRestaurant(r => r.UserId == userId && r.IsActive && r.Score != null).Skip(Convert.ToInt32(pageNumber * PageUtil.PanelMyReviewShownCount)).Take(PageUtil.PanelMyReviewShownCount).ToList();
            //}
            var reviewCount = service.Uow.Reviews.GetCountByUserId(userId);
            var maxPage = Math.Ceiling(reviewCount / Convert.ToDouble(PageUtil.PanelMyReviewShownCount));
            model.PageNumber = pageNumber;
            model.ShownCount = PageUtil.PanelMyReviewShownCount;
            model.MaxPage = maxPage;
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id) {
            var review = service.Uow.Reviews.GetById(id);
            review.IsActive = false;
            service.Uow.Reviews.Update(review);
            service.Uow.Save();
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Update(int id, string reviewContent) {
            var review = service.Uow.Reviews.GetById(id);
            review.Content = reviewContent;
            service.Uow.Reviews.Update(review);
            service.Uow.Save();
            return RedirectToAction("List");
        }

    }
}