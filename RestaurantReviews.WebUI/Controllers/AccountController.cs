using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using RestaurantReviews.BLL.Service;
using RestaurantReviews.Models.Entities.Identity;
using RestaurantReviews.WebUI.Managers;
using RestaurantReviews.WebUI.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace RestaurantReviews.WebUI.Controllers {
    public class AccountController : Controller {
        DataService service;
        public AccountController() {
            service = new DataService();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginVm data) {
            if (ModelState.IsValid) {
                var userManager = service.Uow.Users;

                var user = userManager.FindByName(data.Username);
                if (user != null) {
                    if (user.EmailConfirmed && user.IsActive) {
                        if (userManager.CheckPassword(user, data.Password)) {
                            var identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                            HttpContext.GetOwinContext().Authentication.SignIn(
                                new AuthenticationProperties {
                                    IsPersistent = true
                                }, identity);
                            return Json(new { isSuccess = true });
                        }
                    }
                }
            }
            return Json(new { isSuccess = false });
        }

        public ActionResult Logout() {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SignUp(SignUpVm data) {
            var mailManager = new MailManager();
            if (ModelState.IsValid) {
                var userManager = service.Uow.Users;
                userManager.Create(new AppUser {
                    Email = data.Email,
                    UserName = data.SignupUsername,
                    EmailVerificationCode = Guid.NewGuid().ToString(),
                    ProfilePicPath = "/Content/Images/ProfilePics/default-pp.jpg",
                    ProfilePicUploadDate = DateTime.Now,
                    IsActive = true
                }, data.SignupPassword);

                var user = userManager.FindByName(data.SignupUsername);
                userManager.AddToRole(user.Id, "default_user");

                Uri uri = new Uri(Request.Url.ToString());
                string mailSubject = "Restoran İnceleme E-posta Aktivasyonu";
                string mailBody = "Lütfen bağlantıya tıklayarak üyeliğinizi aktif ediniz: " + uri.GetLeftPart(UriPartial.Authority) + "/account/mailverify?username=" + user.UserName + "&confirmationCode=" + user.EmailVerificationCode;
                mailManager.SendMail("restaurantreviewstr@gmail.com", "xxxxxxxx", data.Email, mailSubject, mailBody);

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult MailVerify(string username, string confirmationCode) {
            var user = service.Uow.Users.FindByName(username);
            if (user.EmailVerificationCode == confirmationCode) {
                user.EmailConfirmed = true;
            }
            service.Uow.Users.Update(user);
            service.Uow.Save();
            return RedirectToAction("Index", "Home");
        }
    }
}