using Microsoft.AspNet.Identity;
using RestaurantReviews.BLL.Service;
using RestaurantReviews.Models.Entities;
using RestaurantReviews.WebUI.Models;
using System;
using System.Web.Helpers;
using System.Web.Mvc;

namespace RestaurantReviews.WebUI.Controllers {
    public class ImageController : Controller {
        DataService service;
        public ImageController() {
            service = new DataService();
        }

        public ActionResult Add(AddImageVm data) {
            WebImage img = new WebImage(data.RestaurantImage.InputStream);
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
            img.FileName = fileName + "." + img.ImageFormat;
            var restaurantImage = new RestaurantImage();
            restaurantImage.RestaurantId = data.RestaurantId;
            restaurantImage.ImgFilePath = "/Content/Images/RestaurantPics/" + img.FileName;
            restaurantImage.UploaderId = User.Identity.GetUserId();
            img.Save(Server.MapPath("/Content/Images/RestaurantPics/") + img.FileName);
            service.Uow.RestaurantImages.Insert(restaurantImage);
            service.Uow.Save();
            return RedirectToAction("Detail", "Restaurant", new { id = data.RestaurantId });
        }
    }
}