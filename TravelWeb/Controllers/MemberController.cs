using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        tMemberEntities db = new tMemberEntities();
        // GET: Member
        public ActionResult Index2()
        {
            return View();
        }
        public ActionResult Logout() 
        { 
            FormsAuthentication.SignOut(); //登出
            return RedirectToAction("Login", "Home");
        }

        public ActionResult MemberList() //顯示所有會員
        {
            var members =
                db.tMember.OrderByDescending(x => x.Id).ToList();
            return View("../Member/MemberList", "_Layoutmember", members);
        }
        public ActionResult ShowEdit(int Id)
        {
            var member = 
                db.tMember.Where(m => m.Id == Id).FirstOrDefault(); //讀資料庫特定Id的欄位資料
            return View(member);
        }
        [HttpPost]
        public ActionResult Edit(int Id, string UserId, string Password, string Name, string Email)
        {
            var member = db.tMember.Where(m => m.Id == Id).FirstOrDefault(); //讀資料庫特定Id的欄位資料
            member.Id = Id;
            member.UserId = UserId;
            member.Password = Password;
            member.Name = Name;
            member.Email = Email;
            db.SaveChanges();
            return RedirectToAction("MemberList");
        }
        public ActionResult DeleteMember(int Id) 
        {
            var member = db.tMember.FirstOrDefault(x => x.Id == Id);
            db.tMember.Remove(member);
            db.SaveChanges();
            return RedirectToAction("MemberList");
        }
        public ActionResult MemberProfile()
        {
            return View();
        }
        public ActionResult EditProfile() 
        {
            return View();
        }
    }
}