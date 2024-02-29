using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTW_FFOOD.Models;
using PagedList;
namespace LTW_FFOOD.Areas.Admin.Controllers
{
    public class TaikhoanController : Controller
    {
        // GET: Admin/Taikhoan
        LTWEB_FFoodEntities db = new LTWEB_FFoodEntities();
        public ActionResult taikhoan(int? page, int? pagesize, string search = "")
        {
            if (Session["UserID"] == null)
            {
                // Chuyển hướng người dùng đến trang đăng nhập
                return RedirectToAction("Login", "DangNhap");
            }
            if (page == null)
            {
                page = 1;
            }
            if (pagesize == null)
            {
                pagesize = 5;
            }

            var account = db.Account.Where(a => a.username.Contains(search)).ToList();
            return View(account.ToPagedList((int)page, (int)pagesize));
        }
       
        public ActionResult ThemmoiTK()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemmoiTK(Account account)
        {
            LTWEB_FFoodEntities db = new LTWEB_FFoodEntities();
            db.Account.Add(account);
            db.SaveChanges();
            return RedirectToAction("taikhoan");
        }
        public ActionResult EditTK(int id)
        {
            LTWEB_FFoodEntities db = new LTWEB_FFoodEntities();
            Account account = db.Account.Where(r => r.account_id == id).FirstOrDefault();
            return View(account);
        }
        [HttpPost]
        public ActionResult EditTK(Account account)
        {
            LTWEB_FFoodEntities db = new LTWEB_FFoodEntities();
            Account accounts = db.Account.Where(r => r.account_id == account.account_id).FirstOrDefault();
            accounts.account_id = account.account_id;
            accounts.username = account.username;
            accounts.password = account.password;
            accounts.email = account.email;
            accounts.phone = account.phone;
            accounts.id_role = account.id_role;
            db.SaveChanges();
          return  RedirectToAction("taikhoan");
        }
        public ActionResult DeleteTK(int id)
        {
            LTWEB_FFoodEntities db = new LTWEB_FFoodEntities();
            Account account = db.Account.Where(r => r.account_id == id).FirstOrDefault();
            return View(account);
        }
        [HttpPost]
        public ActionResult DeleteTK(Account account)
        {

            LTWEB_FFoodEntities db = new LTWEB_FFoodEntities();
            Account accounts = db.Account.Where(r => r.account_id == account.account_id).FirstOrDefault();
            db.Account.Remove(accounts);
            db.SaveChanges();
            return RedirectToAction("taikhoan");
        }
    }
}