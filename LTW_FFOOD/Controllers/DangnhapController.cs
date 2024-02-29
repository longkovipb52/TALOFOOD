using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LTW_FFOOD.Common;
using LTW_FFOOD.Models;
namespace LTW_FFOOD.Controllers
{
    public class DangnhapController : Controller
    {
        // GET: Dangnhap
        public ActionResult Login()
        {
            return View();
        }
        private bool Ktrataikhoan(string username, string password)
        {
            using (var context = new LTWEB_FFoodEntities())
            {
                var user = context.Account.SingleOrDefault(u => u.email == username || u.phone == username);
                if (user != null)
                {
                    if (user.password == password)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public ActionResult Dangnhap(string username, string password)
        {
            using (var context = new LTWEB_FFoodEntities())
            {
                var user = context.Account.SingleOrDefault(u => u.email == username || u.phone == username);
                if (user != null)
                {
                    if (user.password == password)
                    {
                       
                        if (user.id_role == 0)
                        {
                            Session["Username"] = user.username;
                            Session["UserID"] = user.account_id;
                            return RedirectToAction("taikhoan", "TaiKhoan", new { area = "Admin" });
                        }
                        else
                        {
                            Session["Username"] = user.username;
                            Session["UserID"] = user.account_id;
                            return RedirectToAction("Home", "Home");
                        }
                    }
                }
            }

            TempData["LoiDangNhap"] = "Thông tin đăng nhập sai";
            return RedirectToAction("Login");
        }

        public ActionResult Dangxuat()
        {
            
            Session.Remove("Username");
            return RedirectToAction("Home", "Home");
        }
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Mật khẩu xác nhận không khớp với mật khẩu đã nhập.");
                    return View(model);
                }
                using (var db = new LTWEB_FFoodEntities())
                {
                   
                    var account = new Account()
                    {
                        username = model.Ho + " " + model.TenDem + " " + model.Ten,
                        email = model.Email,
                        password = model.Password,
                        id_role = 1,
                        phone = model.Phone,
                        address = model.Address
                        
                    };
                    db.Account.Add(account);
                    db.SaveChanges();
                }
                return RedirectToAction("Login");
            }
            ViewBag.LoiDangKy = "Có lỗi xảy ra. Vui lòng kiểm tra lại thông tin đăng ký.";
            return View(model);
        }
    }
 }