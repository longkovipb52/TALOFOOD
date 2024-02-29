using LTW_FFOOD.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using LTW_FFOOD.Models;
namespace LTW_FFOOD.Controllers
{
    public class DangNhap2Controller : Controller
    {
        // GET: Base
        public ActionResult Login2()
        {
            return View();
        }
        public ActionResult Dangnhap2(string username, string password)
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
            TempData["ErrorMessage"] = "Thông tin đăng nhập sai";
            return RedirectToAction("Login2");
        }
    }
}