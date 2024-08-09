using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelWeb.Models;
using System.Web.Security;

namespace TravelWeb.Controllers
{
    public class HomeController : Controller
    {
        
        tMemberEntities db = new tMemberEntities();
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(tMember pMember)
        {
            if(ModelState.IsValid == false)
            {
                return View();
            }
            var member = db.tMember
                .Where(m => m.UserId == pMember.UserId)
                .FirstOrDefault();
            if (member == null)
            {
                db.tMember.Add(pMember);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            ViewBag.Message = "此帳號已有人使用，註冊失敗";
            return View();
        }
        public ActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Login(string UserId, string Password)
        {
            var member = db.tMember
                .Where(m => m.UserId == UserId && m.Password == Password)
                .FirstOrDefault();
            if (member == null)
            {
                ViewBag.Message = "帳密錯誤，登入失敗";
                return View();
            }
            FormsAuthentication.RedirectFromLoginPage(UserId, true);
            return RedirectToAction("Index2", "Member");
        }
    }
}