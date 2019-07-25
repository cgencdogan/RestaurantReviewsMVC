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
            if (data.RestaurantImage.ContentType == "image/jpeg" || data.RestaurantImage.ContentType == "image/jpg" || data.RestaurantImage.ContentType == "image/png") {
                if (data.RestaurantImage.ContentLength < 2000000) {
                    WebImage img = new WebImage(data.RestaurantImage.InputStream);
                    if (img.Height > 500) {
                        img.Resize(1400, 500, true, false);
                    }
                    //Örnek dosya adı: 13_201907251137437654386.jpeg
                    string fileName = data.RestaurantId + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
                    string folderPath = "/Content/Images/RestaurantPics/" + data.RestaurantId + "/";
                    img.FileName = fileName + "." + img.ImageFormat;
                    var restaurantImage = new RestaurantImage();
                    restaurantImage.RestaurantId = data.RestaurantId;
                    restaurantImage.ImgFilePath = folderPath + img.FileName;
                    restaurantImage.UploaderId = User.Identity.GetUserId();
                    //RestaurantPics klasöründe adı restoran id'si olan klasör yoksa oluştur:
                    System.IO.Directory.CreateDirectory(Server.MapPath(folderPath));
                    img.Save(Server.MapPath(folderPath) + img.FileName);
                    service.Uow.RestaurantImages.Insert(restaurantImage);
                    service.Uow.Save();
                    return RedirectToAction("Detail", "Restaurant", new { id = data.RestaurantId });
                }
                else {
                    return Json(new { invalidSize = true });
                }
            }
            else {
                return Json(new { invalidFormat = true });
            }
        }
    }
}