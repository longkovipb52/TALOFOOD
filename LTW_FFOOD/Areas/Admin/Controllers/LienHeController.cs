using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTW_FFOOD.Models;
using PagedList;
namespace LTW_FFOOD.Areas.Admin.Controllers
{
    public class LienHeController : Controller
    {
        // GET: Admin/LienHe
        LTWEB_FFoodEntities db = new LTWEB_FFoodEntities();
        public ActionResult lienhe(int? page, int? pagesize,string search="")
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
            var contact = db.Contact.Where(c=>c.name.Contains(search)).ToList();
            return View(contact.ToPagedList((int)page,(int)pagesize));
        }
    }
}