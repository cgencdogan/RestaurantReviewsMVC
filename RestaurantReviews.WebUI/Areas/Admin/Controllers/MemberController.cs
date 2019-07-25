using Microsoft.AspNet.Identity;
using RestaurantReviews.BLL.Service;
using RestaurantReviews.WebUI.Areas.Admin.Models;
using RestaurantReviews.WebUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RestaurantReviews.WebUI.Areas.Admin.Controllers {
    [Authorize(Roles = "admin")]
    public class MemberController : Controller {
        DataService service;
        public MemberController() {
            service = new DataService();
        }

        public ActionResult List(string searchWord, bool confirmed = true, int pageNumber = 0) {
            int memberCount;
            var model = new PanelMemberListVm();
            model.MemberList = new List<PanelMemberVm>();
            if (searchWord != null) {
                if (confirmed == true) {
                    memberCount = service.Uow.Users.Users.Where(u => u.EmailConfirmed && u.IsActive && u.UserName.Contains(searchWord)).Count();
                    model.MemberList = GetUserList(true, true, searchWord, pageNumber);
                }
                else {
                    memberCount = service.Uow.Users.Users.Where(u => !u.EmailConfirmed && u.IsActive && u.UserName.Contains(searchWord)).Count();
                    model.MemberList = GetUserList(false, true, searchWord, pageNumber);
                }
            }
            else {
                if (confirmed == true) {
                    memberCount = service.Uow.Users.Users.Where(u => u.EmailConfirmed && u.IsActive).Count();
                    model.MemberList = GetUserList(true, true, searchWord, pageNumber);
                }
                else {
                    memberCount = service.Uow.Users.Users.Where(u => !u.EmailConfirmed && u.IsActive).Count();
                    model.MemberList = GetUserList(false, true, searchWord, pageNumber);
                }
            }
            model.SearchWord = searchWord;
            var maxPage = Math.Ceiling(memberCount / Convert.ToDouble(PageUtil.PanelMemberShownCount));
            model.ShownCount = PageUtil.PanelMemberShownCount;
            model.PageNumber = pageNumber;
            model.MaxPage = maxPage;
            model.IsConfirmed = confirmed;
            return View(model);
        }

        public ActionResult PassiveList(string searchWord, int pageNumber = 0) {
            int memberCount;
            var model = new PanelMemberListVm();
            model.MemberList = new List<PanelMemberVm>();

            memberCount = service.Uow.Users.Users.Where(u => !u.IsActive).Count();
            model.MemberList = GetUserList(true, false, searchWord, pageNumber);

            var maxPage = Math.Ceiling(memberCount / Convert.ToDouble(PageUtil.PanelMemberShownCount));
            model.ShownCount = PageUtil.PanelMemberShownCount;
            model.PageNumber = pageNumber;
            model.MaxPage = maxPage;
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeUserPassword(ChangePasswordVm data) {
            service.Uow.Users.RemovePassword(data.UserId);
            service.Uow.Users.AddPassword(data.UserId, data.Password);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult DeactivateUser(string id) {
            var appUser = service.Uow.Users.FindById(id);
            appUser.IsActive = false;
            service.Uow.Users.Update(appUser);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult ActivateUser(string id) {
            var appUser = service.Uow.Users.FindById(id);
            appUser.IsActive = true;
            service.Uow.Users.Update(appUser);
            return RedirectToAction("PassiveList");
        }

        [HttpPost]
        public ActionResult PermDeleteUser(string id) {
            var appUser = service.Uow.Users.FindById(id);
            service.Uow.Reviews.DeleteByUserId(id);
            service.Uow.RestaurantImages.DeleteByUserId(id);
            service.Uow.Users.Delete(appUser);
            service.Uow.Save();
            return RedirectToAction("PassiveList");
        }

        public List<PanelMemberVm> GetUserList(bool isEmailConfirmed, bool isActive, string searchWord, int pageNumber) {
            var memberList = new List<PanelMemberVm>();
            var members = new List<PanelMemberVm>();
            if (isActive == true) {
                if (searchWord != null) {
                    members = service.Uow.Users.Users.Where(u => u.EmailConfirmed == isEmailConfirmed && u.IsActive == isActive && u.UserName.Contains(searchWord)).ToList().Skip(pageNumber * PageUtil.PanelMemberShownCount).Take(PageUtil.PanelMemberShownCount).Select(item => new PanelMemberVm {
                        AppUserId = item.Id,
                        Username = item.UserName,
                        Email = item.Email
                    }).ToList();
                }
                else {
                    members = service.Uow.Users.Users.Where(u => u.EmailConfirmed == isEmailConfirmed && u.IsActive == isActive).ToList().Skip(pageNumber * PageUtil.PanelMemberShownCount).Take(PageUtil.PanelMemberShownCount).Select(item => new PanelMemberVm {
                        AppUserId = item.Id,
                        Username = item.UserName,
                        Email = item.Email
                    }).ToList();
                }
            }
            else {
                if (searchWord != null) {
                    members = service.Uow.Users.Users.Where(u => u.IsActive == isActive && u.UserName.Contains(searchWord)).ToList().Skip(pageNumber * PageUtil.PanelMemberShownCount).Take(PageUtil.PanelMemberShownCount).Select(item => new PanelMemberVm {
                        AppUserId = item.Id,
                        Username = item.UserName,
                        Email = item.Email
                    }).ToList();
                }
                else {
                    members = service.Uow.Users.Users.Where(u => u.IsActive == isActive).ToList().Skip(pageNumber * PageUtil.PanelMemberShownCount).Take(PageUtil.PanelMemberShownCount).Select(item => new PanelMemberVm {
                        AppUserId = item.Id,
                        Username = item.UserName,
                        Email = item.Email
                    }).ToList();
                }
            }
            foreach (var item in members) {
                var panelMember = new PanelMemberVm();
                panelMember.AppUserId = item.AppUserId;
                panelMember.Username = item.Username;
                panelMember.Email = item.Email;
                panelMember.ReviewCount = service.Uow.Reviews.GetCountByUserId(panelMember.AppUserId);
                memberList.Add(panelMember);
            }
            return memberList;
        }
    }
}