using Microsoft.AspNet.Identity;
using RestaurantReviews.BLL.Service;
using RestaurantReviews.WebUI.Areas.Member.Models;
using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace RestaurantReviews.WebUI.Areas.Member.Controllers {
    [Authorize(Roles = "admin,moderator,default_user")]
    public class MemberController : Controller {
        DataService service;
        public MemberController() {
            service = new DataService();
        }

        public ActionResult Details() {
            var model = new DetailsVm();
            model.AppUser = service.Uow.Users.FindById(User.Identity.GetUserId());
            model.ReviewCount = service.Uow.Reviews.GetCountByUserId(model.AppUser.Id);
            return View(model);
        }

        public ActionResult ChangeMail() {
            var model = service.Uow.Users.FindById(User.Identity.GetUserId());
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeMail(string email) {
            var user = service.Uow.Users.FindById(User.Identity.GetUserId());
            user.Email = email;
            service.Uow.Users.Update(user);
            service.Uow.Save();
            return RedirectToAction("Details");
        }

        public ActionResult ChangePassword() {
            var model = service.Uow.Users.FindById(User.Identity.GetUserId());
            return View(model);
        }

        public ActionResult CheckPassword(ChangePasswordVm data) {
            var userId = User.Identity.GetUserId();
            var appUser = service.Uow.Users.FindById(userId);
            if (!service.Uow.Users.CheckPassword(appUser, data.CurrentPassword)) {
                return Json(new {
                    isPasswordCorrect = false,
                    isNewPasswordConfirmed = true
                });
            }
            else if (data.NewPassword != data.NewPasswordConfirm) {
                return Json(new {
                    isNewPasswordConfirmed = false,
                    isPasswordCorrect = true
                });
            }
            else {
                return Json(new {
                    isNewPasswordConfirmed = true,
                    isPasswordCorrect = true
                });
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordVm data) {
            var user = service.Uow.Users.FindById(User.Identity.GetUserId());
            if (service.Uow.Users.CheckPassword(user, data.CurrentPassword) && (data.NewPassword == data.NewPasswordConfirm)) {
                service.Uow.Users.ChangePassword(user.Id, data.CurrentPassword, data.NewPassword);
            }
            return RedirectToAction("Details");
        }

        public ActionResult ChangeAvatar() {
            var model = service.Uow.Users.FindById(User.Identity.GetUserId());
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeAvatar(HttpPostedFileBase profilePic) {
            var user = service.Uow.Users.FindById(User.Identity.GetUserId());
            if (System.IO.File.Exists(Server.MapPath(user.ProfilePicPath))) {
                System.IO.File.Delete(Server.MapPath(user.ProfilePicPath));
            }
            WebImage newProfilePic = new WebImage(profilePic.InputStream);
            newProfilePic.Resize(200, 200, false);
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + user.UserName;
            newProfilePic.FileName = fileName + "." + newProfilePic.ImageFormat;
            user.ProfilePicPath = "/Content/Images/ProfilePics/" + newProfilePic.FileName;
            user.ProfilePicUploadDate = DateTime.Now;
            newProfilePic.Save(Server.MapPath("~/Content/Images/ProfilePics/") + newProfilePic.FileName);
            service.Uow.Users.Update(user);
            service.Uow.Save();
            return RedirectToAction("Details");
        }

        public ActionResult PublicProfile(string username) {
            var model = new PublicProfileVm();

            var appUser = service.Uow.Users.FindByName(username);
            var likedReview = service.Uow.Reviews.GetMostLikedRestaurantIdByUserId(appUser.Id);
            if (likedReview != null) {
                model.LikedRestaurantName = service.Uow.Restaurants.GetById(likedReview.RestaurantId).Name;
                model.LikedRestaurantId = likedReview.RestaurantId;
                model.LikedRestaurantScore = likedReview.Score;
            }
            else {
                model.LikedRestaurantName = "Bu kullanıcı hiçbir restorana puan vermemiş.";
                model.LikedRestaurantId = 0;
                model.LikedRestaurantScore = 0;
            }
            var dislikedReview = service.Uow.Reviews.GetMostDislikedRestaurantIdByUserId(appUser.Id);
            if (dislikedReview != null) {
                model.DislikedRestaurantName = service.Uow.Restaurants.GetById(dislikedReview.RestaurantId).Name;
                model.DislikedRestaurantId = dislikedReview.RestaurantId;
                model.DislikedRestaurantScore = dislikedReview.Score;
            }
            else {
                model.DislikedRestaurantName = "Bu kullanıcı hiçbir restorana puan vermemiş.";
                model.DislikedRestaurantId = 0;
                model.DislikedRestaurantScore = 0;
            }
            var reviewCount = service.Uow.Reviews.GetCountByUserId(appUser.Id);

            model.AppUserId = appUser.Id;
            model.Username = appUser.UserName;
            model.ProfilePicPath = appUser.ProfilePicPath;
            model.ReviewCount = reviewCount;
            return View(model);
        }

        public ActionResult Deactivate() {
            var appUserId = User.Identity.GetUserId();
            var appUser = service.Uow.Users.FindById(appUserId);
            appUser.IsActive = false;
            service.Uow.Users.Update(appUser);
            service.Uow.Save();
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
    }
}